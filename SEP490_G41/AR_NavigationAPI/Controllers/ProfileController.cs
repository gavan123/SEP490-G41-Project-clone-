using BusinessObject.DTO;
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

        // GET: api/Profile/1
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
        // PUT: api/Profile/1
        [HttpPut("{id}")]
        public IActionResult UpdateProfileById(int id, MemberUpdateDTO progfile)
        {
            var tmpProfile = _profileRepository.GetMemberById(id);
            if (tmpProfile == null)
            {
                return NotFound();
            }
            _profileRepository.UpdateProfile(progfile);

            return Ok();
        }
    }
}
