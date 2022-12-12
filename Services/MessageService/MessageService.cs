using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using disclodo.Data;
using disclodo.Dtos.Message;
using disclodo.MessageExtensions;
namespace disclodo.Services.MessageService;

public class MessageService : IMessageService
{
    private readonly DataContext _context;
    public MessageService(DataContext context)
    {
        _context = context;
    }
    public async Task<GetMessageDto?> AddMessage(PostMessageDto message)
    {
        var author = await _context.User.FindAsync(message.AuthorId);
        var channel = await _context.Channel.FindAsync(message.ChannelId);
        if (author == null || channel == null) return null;

        var createdMessage = message.ToMessage(author, channel);
        await _context.Message.AddAsync(createdMessage);
        await _context.SaveChangesAsync();
        return createdMessage.ToGetMessageDto();
    }
    public async Task<List<GetMessageDto>> GetChannelMessages(Guid channelId)
    {
        var messages = await _context.Message.Include(m => m.Channel).Where(m => m.Channel.Id == channelId).Include(m => m.Author).ToListAsync();
        return messages.Select(m => m.ToGetMessageDto()).ToList();
    }

    public async Task<GetMessageDto> UpdateMessage(PutMessageDto message, int id)
    {
        var messageToUpdate = await _context.Message.FindAsync(id);
        if (messageToUpdate == null) return null!;
        messageToUpdate.ApplyPutMessageDto(message);
        await _context.SaveChangesAsync();
        return messageToUpdate.ToGetMessageDto();
    }

    public async Task<bool> DeleteMessage(int id)
    {
        var message = await _context.Message.FindAsync(id);
        if (message == null) return false;
        _context.Message.Remove(message);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0;
    }
}
