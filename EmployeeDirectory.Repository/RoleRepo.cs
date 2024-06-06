using EmployeeDirectory.Repository.ScaffoldData;
using EmployeeDirectory.Repository.ScaffoldData.DataConcerns;
using EmployeeDirectory.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private readonly EmployeesDbContext _dbContext;
        public RoleRepo(EmployeesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Role role)
        {
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
        }

        public bool DeleteRole(int id)
        {
            var role = _dbContext.Roles.FirstOrDefault(role => role.Id == id);
            if(role == null) return false;
            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Role> GetAll()
        {
            return _dbContext.Roles
                .Include(r => r.Department)
                .Include(r => r.Location)
                .ToList();
        }

        public Role? GetById(int id)
        {
            return _dbContext.Roles
                .Include(r => r.Department)
                .Include(r => r.Location)
                .FirstOrDefault(role => role.Id == id);
        }

        public List<Role> GetRoleInDepartment(int departmntId)
        {
            return _dbContext.Roles.Where(role => role.DepartmentId == departmntId).ToList();
        }
    }
}
