using DineMasterApi.DTO;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

       public IRolesRepo repo;
       public IUser urepo;
        public UserController(IRolesRepo repo, IUser urepo)
        {
            this.repo = repo;
            this.urepo = urepo;
        }



        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto dto)
        {
            var role = await repo.GetRoleByNameAsync("Customer");
            if (role == null) return BadRequest("Default role not found.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = role.RoleId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await urepo.RegisterUserAsync(user);

            return Ok(new UserDto
            {
                UserId = created.UserId,
                Username = created.Username,
                Email = created.Email,
                Phone = created.Phone,
                RoleName = role.RoleName,
                CreatedAt = created.CreatedAt
            });
        }

        [HttpPost("AddStaff")]
        public async Task<ActionResult<UserDto>> AddStaff(UserAdminCreateDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Phone = dto.Phone,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await urepo.AddStaffUserAsync(user);

            return Ok(new UserDto
            {
                UserId = created.UserId,
                Username = created.Username,
                Email = created.Email,
                Phone = created.Phone,
                RoleName = (await repo.GetByIdAsync(dto.RoleId))?.RoleName,
                CreatedAt = created.CreatedAt
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await urepo.GetAllUsersAsync();
            return Ok(users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                Phone = u.Phone,
                RoleName = u.Role?.RoleName,
                CreatedAt = u.CreatedAt
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var u = await urepo.GetUserByIdAsync(id);
            if (u == null) return NotFound();

            return Ok(new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                Phone = u.Phone,
                RoleName = u.Role?.RoleName,
                CreatedAt = u.CreatedAt
            });
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> Update(UserUpdateDto dto)
        {
            var updatedUser = await urepo.UpdateUserAsync(new User
            {
                UserId = dto.UserId,
                Username = dto.Username,
                Email = dto.Email,
                Phone = dto.Phone,
                RoleId = dto.RoleId
            });

            if (updatedUser == null) return NotFound();

            var role = await repo.GetByIdAsync(updatedUser.RoleId);

            return Ok(new UserDto
            {
                UserId = updatedUser.UserId,
                Username = updatedUser.Username,
                Email = updatedUser.Email,
                Phone = updatedUser.Phone,
                RoleName = role?.RoleName,
                CreatedAt = updatedUser.CreatedAt
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await urepo.DeleteUserAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }



    }
}
