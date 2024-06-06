using EmployeeDirectory.Repository.ScaffoldData.DataConcerns;
using EmployeeDirectory.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectoryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepo _statusRepo;

        public StatusController(IStatusRepo statusRepo)
        {
            _statusRepo = statusRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _statusRepo.Get());
        }

        [HttpPost]
        public IActionResult Add(Status status)
        {
            _statusRepo.Add(status);
            return Created();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _statusRepo.Remove(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
