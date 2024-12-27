using Models;

namespace OdevDagitim.Models
{
    public class AssignmentSubmission : BaseEntity
    {
        public int AssignmentId { get; set; }
        public string UserId { get; set; }
        public string SubmissionPath { get; set; }  // Dosya yolu
        public string? Description { get; set; }    // Öğrencinin açıklaması
        public DateTime SubmissionDate { get; set; }
        public bool IsLate { get; set; }           // Geç teslim kontrolü

        // Navigation properties
        public Assignment Assignment { get; set; }
        public AppUser User { get; set; }
    }
} 