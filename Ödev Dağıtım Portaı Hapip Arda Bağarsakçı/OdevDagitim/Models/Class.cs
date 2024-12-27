using Models;

namespace OdevDagitim.Models
{
    public class Class : BaseEntity
    {
        public string ClassName { get; set; }
        public string Description { get; set; }
        public ICollection<AppUser> Users { get; set; }
    }
} 