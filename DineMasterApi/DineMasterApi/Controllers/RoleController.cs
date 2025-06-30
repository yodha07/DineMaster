using DineMasterApi.DTO;
using DineMasterApi.Models;
using DineMasterApi.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DineMasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        IRolesRepo repo;
        public RoleController(IRolesRepo repo)
        {
            this.repo = repo;
        }



        
        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await repo.fetchallroles();
            return Ok(roles.Select(r => new RoleDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            }));
        }

        
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var role = await repo.GetByIdAsync(id);
            if (role == null) return NotFound();

            return Ok(new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            });
        }

        
        [HttpPost("AddRole")]
        public async Task<ActionResult<RoleDto>> Create(RoleCreateDto dto)
        {
            var role = new Role { RoleName = dto.RoleName };
            var created = await repo.AddAsync(role);

            return CreatedAtAction(nameof(GetById), new { id = created.RoleId }, new RoleDto
            {
                RoleId = created.RoleId,
                RoleName = created.RoleName
            });
        }

        // PUT: api/role
        [HttpPut("UpdateRole")]
        public async Task<ActionResult<RoleDto>> Update(RoleUpdateDto dto)
        {
            var role = new Role { RoleId = dto.RoleId, RoleName = dto.RoleName };
            var updated = await repo.UpdateAsync(role);
            if (updated == null) return NotFound();

            return Ok(new RoleDto
            {
                RoleId = updated.RoleId,
                RoleName = updated.RoleName
            });
        }

        // DELETE: api/role/5
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await repo.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok("Role Deleted Succesfully");
        }





    }
}
