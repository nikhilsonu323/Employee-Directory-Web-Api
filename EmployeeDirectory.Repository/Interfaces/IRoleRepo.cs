using EmployeeDirectory.Repository.ScaffoldData.DataConcerns;

namespace EmployeeDirectory.Repository.Interfaces
{
    public interface IRoleRepo
    {
        void Add(Role role);

        List<Role> GetAll();

        Role? GetById(int id);

        bool DeleteRole(int id);

        List<Role> GetRoleInDepartment(int departmntId);
    }
}