using AspNetCoreHero.ToastNotification.Notyf.Models;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;

namespace OdevDagitim.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config, IPasswordHasher<AppUser> passwordHasher) : base(options)
        {
            _config = config;
            _passwordHasher = passwordHasher;
        }


        public DbSet<Class> Classes { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public string MD5Hash(string pass)
        {
            var salt = _config.GetValue<string>("AppSettings:MD5Salt");
            var password = pass + salt;
            var hashed = password.MD5();
            return hashed;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Rolleri tanımlayalım
            var adminRoleId = Guid.NewGuid().ToString();
            var teacherRoleId = Guid.NewGuid().ToString();
            var studentRoleId = Guid.NewGuid().ToString();

            // Admin kullanıcısı için ID
            var adminUserId = Guid.NewGuid().ToString();

            // Rolleri oluşturalım
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = adminRoleId,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                },
                new AppRole
                {
                    Id = teacherRoleId,
                    Name = "Teacher",
                    NormalizedName = "TEACHER",
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                },
                new AppRole
                {
                    Id = studentRoleId,
                    Name = "Ogrenci",
                    NormalizedName = "OGRENCI",
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                }
            );

            // Admin kullanıcısını oluşturalım
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = _passwordHasher.HashPassword(null, "admin"),
                SecurityStamp = string.Empty,
                Created = DateTime.Now,
                Updated = DateTime.Now
            });

            // Admin kullanıcısına admin rolünü atayalım
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
            );

            // Assignment ilişkileri
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Class)
                .WithMany()
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Teacher)
                .WithMany(u => u.TeacherAssignments)
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // AssignmentSubmission ilişkileri
            modelBuilder.Entity<AssignmentSubmission>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssignmentSubmission>()
                .HasOne(s => s.User)
                .WithMany(u => u.Submissions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User ilişkileri
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Class)
                .WithMany()
                .HasForeignKey(u => u.ClassId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
