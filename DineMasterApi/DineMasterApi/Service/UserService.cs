using DineMasterApi.Data;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Service
{
    public class UserService:IUser
    {


       public ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<User> AddStaffUserAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await db.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await db.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var existing = await db.Users.FindAsync(user.UserId);
            if (existing == null) return null;

            existing.Username = user.Username;
            existing.Email = user.Email;
            existing.Phone = user.Phone;
            existing.RoleId = user.RoleId;

            await db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null) return false;

            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return true;
        }




    }
}
