using OdevDagitim.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Identity;

namespace OdevDagitim.Repositories
{
    public class UserRepository : GenericRepository<AppUser>
    {
        private readonly UserManager<AppUser> _userManager;
        
        public UserRepository(AppDbContext context, UserManager<AppUser> userManager) : base(context, context.Users)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> GetTeachersAsync()
        {
            var users = await _context.Users
                .Include(u => u.Class)
                .ToListAsync();

            var teachers = new List<AppUser>();
            
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "teacher"))
                {
                    teachers.Add(user);
                }
            }

            return teachers.OrderBy(u => u.UserName);
        }

        public async Task<IEnumerable<AppUser>> GetUsersWithClassAsync()
        {
            return await _context.Users
                .Include(u => u.Class)
                .ToListAsync();
        }
    }
}
