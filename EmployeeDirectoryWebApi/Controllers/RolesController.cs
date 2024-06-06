using EmployeeDirectory.Concerns;
using EmployeeDirectory.Concerns.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpPost("")]
        public IActionResult Add(RoleDTO role)
        {
            _roleServices.AddRole(role);
            return Created();
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role = _roleServices.GetRole(id);
            if (role == null) { return NotFound(); }
            return Ok(role);
        }

        [HttpGet("")]
        public IActionResult GetRoles()
        {
            var roles = _roleServices.GetRoles();
            return Ok(roles);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoleById(int id)
        {
            bool isDeleted = _roleServices.DeleteRole(id);
            if (isDeleted == false) { return NotFound(); }
            return NoContent();
        }

        [HttpGet("Department/{departmntId}")]
        public IActionResult GetRoleInDepartment(int departmntId)
        {
            return Ok(_roleServices.GetRoleInDepartment(departmntId));
        }
    }
}
