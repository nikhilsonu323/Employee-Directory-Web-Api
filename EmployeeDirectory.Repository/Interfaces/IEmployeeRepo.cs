using EmployeeDirectory.Concerns;
using EmployeeDirectory.Repository.ScaffoldData.DataConcerns;

namespace EmployeeDirectory.Repository.Interfaces
{
    public interface IEmployeeRepo
    {
        bool Add(Employee employee);
        List<Employee> GetAll();
        Employee? GetById(string id);
        bool RemoveById(string id);
        bool Update(Employee newEmployeeDetails);
        List<Employee> GetFilterData(Filter filterData);
        List<Manager> GetManagers(string? currentEmpNo);
    }
}