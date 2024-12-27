using Microsoft.AspNetCore.Identity;
using OdevDagitim.Models;
using System;
using System.Collections.Generic;

namespace Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int? ClassId { get; set; }
        
        // Navigation properties
        public Class Class { get; set; }
        public ICollection<Assignment> TeacherAssignments { get; set; }
        public ICollection<AssignmentSubmission> Submissions { get; set; }
    }
} 