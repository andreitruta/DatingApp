using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LockersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        public LockersController(IDatingRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetLockers()
        {
            var lockers = await _repo.GetLockers();
            return Ok(lockers);
        }

    }
}