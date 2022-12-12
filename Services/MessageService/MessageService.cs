using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using disclodo.Data;
using disclodo.Dtos.Message;
using disclodo.MessageExtensions;
using disclodo.Services.AuthService;
using System.Net.Http.Headers;
using System.Text.Json;

namespace disclodo.Services.MessageService;

public class MessageService : IMessageService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;
    public MessageService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }
    public async Task<GetMessageDto?> AddMessage(PostMessageDto message)
    {
        var author = await _context.User.FindAsync(message.AuthorId);
        var channel = await _context.Channel.FindAsync(message.ChannelId);
        if (author == null || channel == null) return null;

        var createdMessage = message.ToMessage(author, channel);
        await _context.Message.AddAsync(createdMessage);
        await _context.SaveChangesAsync();
        
        PublishMessage(createdMessage.ToGetMessageDto());
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

    public async void PublishMessage(GetMessageDto message)
    {
        var token = _authService.CreatePublisherMercureToken();
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsync("http://localhost:1234/.well-known/mercure", new FormUrlEncodedContent(new[] { 
            new KeyValuePair<string, string>("topic", $"http://localhost:1234/channel/{message.ChannelId}"), 
            new KeyValuePair<string, string>("data", JsonSerializer.Serialize(message))
        }));
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception("Failed to publish message");
        }
    }
}
