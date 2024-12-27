using Models;
using OdevDagitim.Models;

public class Message : BaseEntity
{
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime MessageDate { get; set; }
    public bool IsRead { get; set; }

    // Navigation properties
    public AppUser Sender { get; set; }
    public AppUser Receiver { get; set; }
} 