using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using disclodo.Dtos.Channel;
using disclodo.Models;
namespace disclodo.Services.ChannelService;

public interface IChannelService
{
    public Task<GetChannelDto> AddChannel(PostChannelDto channel);
    public Task<List<GetChannelDto>> GetUserChannels(Guid userId);
    public Task<GetChannelDto> AddUsersToChannel(Guid channelId, List<Guid> userIds);
    public Task<bool> DeleteChannel(Guid id);
}
