using EmployeeDirectory.Concerns;
using EmployeeDirectory.Concerns.Interfaces;
using EmployeeDirectory.Repository.Interfaces;
using EmployeeDirectory.Services.Utilities;

namespace EmployeeDirectory.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepo _roleRepo;

        public RoleServices(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public void AddRole(RoleDTO role)
        {
            _roleRepo.Add(Mapper.MapToRoleData(role));
        }

        public RoleDTO? GetRole(int id)
        {
            var role = Mapper.MapToRoleDTO(_roleRepo.GetById(id));
            return role;
        }

        public List<RoleDTO> GetRoles()
        {
            var roles = Mapper.MapToRoleDTO(_roleRepo.GetAll());
            return roles;
        }

        public bool DeleteRole(int id)
        {
            return _roleRepo.DeleteRole(id);
        }

        public List<RoleDTO> GetRoleInDepartment(int departmntId)
        {
            return Mapper.MapToRoleDTO(_roleRepo.GetRoleInDepartment(departmntId));
        }
    }
}
