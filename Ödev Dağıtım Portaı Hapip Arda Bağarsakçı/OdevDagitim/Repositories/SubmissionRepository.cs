using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OdevDagitim.Models;


namespace YourProject.Repositories
{
    public class SubmissionRepository
    {
        private readonly AppDbContext _context;

        public SubmissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteSubmissionsByAssignmentId(int assignmentId)
        {
            var submissions = await _context.AssignmentSubmissions
                .Where(s => s.AssignmentId == assignmentId)
                .ToListAsync();

            if (submissions.Any())
            {
                // Submission dosyalarını sil
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "submissions");
                
                foreach (var submission in submissions)
                {
                    var submissionPath = Path.Combine(uploadPath, submission.Id.ToString());
                    if (Directory.Exists(submissionPath))
                    {
                        Directory.Delete(submissionPath, true);
                    }
                }

                // Veritabanından submission'ları sil
                _context.AssignmentSubmissions.RemoveRange(submissions);
                await _context.SaveChangesAsync();
            }
        }
    }
} 