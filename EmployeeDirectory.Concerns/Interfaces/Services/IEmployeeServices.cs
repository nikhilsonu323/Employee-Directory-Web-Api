namespace EmployeeDirectory.Concerns.Interfaces
{
    public interface IEmployeeServices
    {
        bool AddEmployee(EmployeeDTO employee);

        bool UpdateEmployee(EmployeeDTO employee);

        bool DeleteEmployee(string id);

        List<EmployeeDTO> GetEmployees();

        EmployeeDTO? GetEmployee(string id);

        List<EmployeeDTO> GetFilterData(Filter filterData);

        List<Manager> GetManagers(string? currentEmpNo);
    }
}