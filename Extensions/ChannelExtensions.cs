using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using disclodo.Dtos.Channel;
using disclodo.Models;
namespace disclodo.ChannelExtensions;

public static class ChannelExtensions
{
    public static Channel ToChannel(this PostChannelDto postChannelDto, List<User> participants)
    {
        return new Channel
        {
            Name = postChannelDto.Name,
            Participants = participants
        };
    }
    public static GetChannelDto ToGetChannelDto(this Channel channel)
    {
        return new GetChannelDto
        {
            Id = channel.Id,
            Name = channel.Name,
            CreatedAt = channel.CreatedAt,
            ParticipantIds = channel.Participants.Select(p => p.Id).ToList()
        };
    }

    public static Channel applyPutChannelDto(this Channel channel, PutChannelDto putChannelDto)
    {
        channel.Name = putChannelDto.Name;
        return channel;
    }

}
