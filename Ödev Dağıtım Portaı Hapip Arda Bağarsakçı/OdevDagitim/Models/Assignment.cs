using Models;

namespace OdevDagitim.Models
{
    public class Assignment : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int ClassId { get; set; }
        public string TeacherId { get; set; }
        public string? FilePath { get; set; }

        // Navigation properties
        public Class Class { get; set; }
        public AppUser Teacher { get; set; }
        public ICollection<AssignmentSubmission> Submissions { get; set; }
    }
} 