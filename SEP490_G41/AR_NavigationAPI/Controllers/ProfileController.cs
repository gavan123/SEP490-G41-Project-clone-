using DataAccess.IRepository;
using DataAccess.IRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController : ODataController
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        // GET: api/buildings/5
        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            var profile = _profileRepository.GetMemberById(id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }
    }
}
