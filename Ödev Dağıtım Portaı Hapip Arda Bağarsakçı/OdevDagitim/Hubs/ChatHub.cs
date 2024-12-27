using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Models;


public class ChatHub : Hub
{
    private readonly MessageRepository _messageRepository;
    private readonly UserManager<AppUser> _userManager;

    public ChatHub(MessageRepository messageRepository, UserManager<AppUser> userManager)
    {
        _messageRepository = messageRepository;
        _userManager = userManager;
    }

    public async Task SendMessage(string receiverId, string message)
    {
        var sender = await _userManager.GetUserAsync(Context.User);
        if (sender != null)
        {
            await Clients.User(receiverId).SendAsync("ReceiveMessage", sender.Id, message);
        }
    }

    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
} 