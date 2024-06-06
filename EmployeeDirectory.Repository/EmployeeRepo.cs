using EmployeeDirectory.Concerns;
using EmployeeDirectory.Repository.ScaffoldData;
using EmployeeDirectory.Repository.ScaffoldData.DataConcerns;
using EmployeeDirectory.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly EmployeesDbContext _dbContext;
        public EmployeeRepo(EmployeesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Employee employee)
        {
            if (GetById(employee.EmpNo) != null) return false;
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Employee> GetAll()
        {
            return _dbContext.Employees
                .Include(e => e.Manager)
                .Include(e => e.Location)
                .Include(e => e.Status)
                .Include(e => e.Role)
                .ThenInclude(e => e.Department)
                .ToList();
        }

        public Employee? GetById(string id)
        {
            return _dbContext.Employees
                .Include(e => e.Location)
                .Include(e => e.Status)
                .Include(e => e.Role)
                .ThenInclude(r => r.Department)
                .Include(e => e.Manager)
                .FirstOrDefault(emp => emp.EmpNo == id);
        }

        public bool RemoveById(string id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(emp => emp.EmpNo == id)!;

            if (employee == null) { return false; }
            
            var employeesToUpdate = _dbContext.Employees.Where(emp => emp.ManagerId == id);
            
            foreach (var employeeToUpdate in employeesToUpdate)
                employeeToUpdate.ManagerId = null;
            
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Update(Employee newEmployeeDetails)
        {
            var existingEmployee = _dbContext.Employees.FirstOrDefault(e => e.EmpNo == newEmployeeDetails.EmpNo);
            if(existingEmployee == null) { return false; }
            _dbContext.Entry(existingEmployee).State = EntityState.Detached;
            _dbContext.Employees.Update(newEmployeeDetails);
            _dbContext.SaveChanges();
            return true;
        }

        public List<Employee> GetFilterData(Filter filterData)
        {
           return  _dbContext.Employees
                .Include(e => e.Manager)
                .Include(e => e.Location)
                .Include(e => e.Status)
                .Include(e => e.Role)
                .ThenInclude(e => e.Department)
                .Where(e =>
                (filterData.Alphabets.Count == 0 || filterData.Alphabets.Contains(e.FirstName.Substring(0, 1).ToUpper())) &&
                (filterData.LocationIds.Count == 0 || filterData.LocationIds.Contains(e.LocationId)) &&
                (filterData.DepartmentIds.Count == 0 || filterData.DepartmentIds.Contains(e.Role.DepartmentId)) &&
                (filterData.StatusIds.Count == 0 || filterData.StatusIds.Contains(e.StatusId)))
                .ToList();
        }

        public List<Manager> GetManagers(string? currentEmpNo)
        {
            return _dbContext.Employees
                .Where(emp => currentEmpNo != null && emp.EmpNo != currentEmpNo)
                .Select(emp => new Manager() { EmpNo = emp.EmpNo, FirstName = emp.FirstName, LastName = emp.LastName })
                .ToList();
        }
    }
}
