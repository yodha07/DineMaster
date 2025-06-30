using DineMasterApi.Data;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.EntityFrameworkCore;

namespace DineMasterApi.Service
{
    public class RolesService : IRolesRepo
    {
        ApplicationDbContext db;
        public RolesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Role>> fetchallroles()
        {
            var data = await db.Roles.ToListAsync();            
            return data;            
            
        }


        public async Task<Role> GetByIdAsync(int id)
        {
            return await db.Roles.FindAsync(id);
        }

        public async Task<Role> AddAsync(Role role)
        {
            db.Roles.Add(role);
            await db.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            var existing = await db.Roles.FindAsync(role.RoleId);
            if (existing == null)
                return null;

            existing.RoleName = role.RoleName;
            await db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await db.Roles.FindAsync(id);
            if (role == null)
                return false;

            db.Roles.Remove(role);
            await db.SaveChangesAsync();
            return true;
        }


    }
}
