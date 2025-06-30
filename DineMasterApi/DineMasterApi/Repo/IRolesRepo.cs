using DineMasterApi.Models;

namespace DineMasterApi.Repo
{
    public interface IRolesRepo
    {

        Task<List<Role>> fetchallroles();
        Task<Role> GetByIdAsync(int id);
        Task<Role> AddAsync(Role role);
        Task<Role> UpdateAsync(Role role);
        Task<bool> DeleteAsync(int id);

    }
}
