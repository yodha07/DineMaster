using DineMasterApi.Models;

namespace DineMasterApi.Repo
{
    public interface IUser
    {
        Task<User> RegisterUserAsync(User user);
        Task<User> AddStaffUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
