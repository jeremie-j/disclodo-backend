using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using disclodo.Dtos.Message;
using disclodo.Models;

namespace disclodo.MessageExtensions;

public static class MessageExtensions
{
    public static GetMessageDto ToGetMessageDto(this Message message)
    {
        return new GetMessageDto
        {
            Id = message.Id,
            Content = message.Content,
            CreatedAt = message.CreatedAt,
            Author = message.Author.Username,
            ChannelId = message.Channel.Id,
        };
    }

    public static Message ToMessage(this PostMessageDto messageDto, User author, Channel channel)
    {
        return new Message
        {
            Content = messageDto.Content,
            Author = author,
            Channel = channel,
        };
    }

    public static Message ApplyPutMessageDto(this Message message, PutMessageDto putMessageDto)
    {
        message.Content = putMessageDto.Content;
        return message;
    }
}
