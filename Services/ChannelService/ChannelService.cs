using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using disclodo.Data;
using disclodo.Dtos.Channel;
using disclodo.ChannelExtensions;
using disclodo.Models;
namespace disclodo.Services.ChannelService;

public class ChannelService : IChannelService
{
    private readonly DataContext _context;
    public ChannelService(DataContext context)
    {
        _context = context;
    }
    public async Task<GetChannelDto> AddChannel(PostChannelDto channel)
    {
        var participants = await _context.User.Where(u => channel.ParticipantIds.Contains(u.Id)).ToListAsync();
        var createdChannel = channel.ToChannel(participants);
        await _context.Channel.AddAsync(createdChannel);
        await _context.SaveChangesAsync();
        return createdChannel.ToGetChannelDto();
    }
    public async Task<List<GetChannelDto>> GetUserChannels(Guid userId)
    {
        var result = await _context.User.Include(u=>u.Channels).Where(u => u.Id == userId).FirstOrDefaultAsync();
        if (result == null)
        {
            return null!;
        }
        return result.Channels.Select(channel => channel.ToGetChannelDto()).ToList();
    }
    public async Task<GetChannelDto> AddUsersToChannel(Guid channelId, List<Guid> userIds)
    {
        var channel = await _context.Channel.FindAsync(channelId);
        if (channel == null)
        {
            return null!;
        }
        var users = await _context.User.Where(u => userIds.Contains(u.Id)).ToListAsync();
        channel.Participants.AddRange(users);
        await _context.SaveChangesAsync();
        return channel.ToGetChannelDto();
    }
    public async Task<bool> DeleteChannel(Guid id)
    {
        var channel = await _context.Channel.FindAsync(id);
        if (channel == null)
        {
            return false;
        }
        _context.Channel.Remove(channel);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0;
    }
}
