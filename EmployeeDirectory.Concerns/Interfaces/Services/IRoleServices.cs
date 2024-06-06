namespace EmployeeDirectory.Concerns.Interfaces
{
    public interface IRoleServices
    {
        void AddRole(RoleDTO role);

        List<RoleDTO> GetRoles();

        RoleDTO? GetRole(int id);

        bool DeleteRole(int id);

        List<RoleDTO> GetRoleInDepartment(int departmntId);
    }
}
