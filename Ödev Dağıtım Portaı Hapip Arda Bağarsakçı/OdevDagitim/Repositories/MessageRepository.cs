using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using OdevDagitim.Models;
using OdevDagitim.Repositories;

public class MessageRepository : GenericRepository<Message>
{
    public MessageRepository(AppDbContext context) : base(context, context.Messages)
    {
    }

    public async Task<List<Message>> GetMessagesBetweenUsers(string userId1, string userId2)
    {
        return await _context.Messages
            .Where(m => 
                (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                (m.SenderId == userId2 && m.ReceiverId == userId1))
            .OrderBy(m => m.MessageDate)
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetUserChats(string userId)
    {
        var userIds = await _context.Messages
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
            .Distinct()
            .ToListAsync();

        return await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();
    }

    public async Task<int> GetUnreadMessageCount(string userId)
    {
        return await _context.Messages
            .Where(m => m.ReceiverId == userId && !m.IsRead)
            .CountAsync();
    }

    public async Task MarkMessagesAsRead(string currentUserId, string senderId)
    {
        var messages = await _context.Messages
            .Where(m => m.SenderId == senderId && m.ReceiverId == currentUserId && !m.IsRead)
            .ToListAsync();

        foreach (var message in messages)
        {
            message.IsRead = true;
        }

        await _context.SaveChangesAsync();
    }
} 