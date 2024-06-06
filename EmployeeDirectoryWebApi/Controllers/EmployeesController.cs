using EmployeeDirectory.Concerns;
using EmployeeDirectory.Concerns.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeServices _employeeService;
        public EmployeesController(IEmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public IActionResult GetEmployeesData()
        {
            var employees = _employeeService.GetEmployees();
            return Ok(employees);
        }


        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(string id)
        {
            var emp = _employeeService.GetEmployee(id);
            if (emp == null) { return Ok(); }
            return Ok(emp);
        }

        [HttpPost("")]
        public IActionResult AddEmployee([FromBody] EmployeeDTO employee)
        {
            if ( !_employeeService.AddEmployee(employee) )
            {
                ModelState.AddModelError("employeeId", "Employee Id already exists.");
                return BadRequest(ModelState);
            }
            return CreatedAtAction("GetEmployeeById", new { id = employee.EmpNo }, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeById(string id)
        {
            var isDeleted = _employeeService.DeleteEmployee(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteEmployees([FromBody] List<string> ids)
        {
            var employeesNotFound = new List<string>();
            foreach (string id in ids)
            {
                var isDeleted = _employeeService.DeleteEmployee(id);
                if (!isDeleted)
                {
                    employeesNotFound.Add(id);
                }
            }
            return Ok(employeesNotFound);
        }

        [HttpPut("")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDTO employee)
        {
            if ( !_employeeService.UpdateEmployee(employee) )
            {
                ModelState.AddModelError("employeeId", "Employee Id Not Found.");
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        [HttpPost("filter")]
        public IActionResult FilterData([FromBody] Filter filterData)
        {
            return Ok(_employeeService.GetFilterData(filterData));
        }

        [HttpGet("managers/{empNo}")]
        public IActionResult EmployeesForManagers(string? empNo)
        {
            return Ok(_employeeService.GetManagers(empNo));
        }
    }
}
