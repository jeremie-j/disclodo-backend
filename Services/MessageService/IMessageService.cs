using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using disclodo.Dtos.Message;
using disclodo.Models;
namespace disclodo.Services.MessageService;

public interface IMessageService
{
    public Task<GetMessageDto> AddMessage(PostMessageDto message);
    public Task<List<GetMessageDto>> GetChannelMessages(Guid userId);
    public Task<GetMessageDto> UpdateMessage(PutMessageDto message, int messageId);
    public Task<bool> DeleteMessage(int id);
}
