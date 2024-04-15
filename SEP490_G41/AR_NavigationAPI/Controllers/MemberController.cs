using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace AR_NavigationAPI.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ODataController
    {
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllMembers()
        {
            var member = _memberRepository.GetAllMembers();
            return Ok(member);
        }


        [HttpGet("name")]
        [EnableQuery]
        public IActionResult SearchMemberByName(string name)
        {
            var members = _memberRepository.SearchMemberByName(name);

            if (members == null)
            {
                return NotFound();
            }

            return Ok(members);
        }


        [HttpGet("status")]
        [EnableQuery]
        public IActionResult SearchMemberByStatus(string status)
        {
            var members = _memberRepository.SearchMemberByStatus(status);

            if (members == null)
            {
                return NotFound();
            }

            return Ok(members);
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(string username, string oldPass, string newPass)
        {
            var check = _memberRepository.Login(username, oldPass);
            if (check == true)
            {
                var member = _memberRepository.ChangePassword(oldPass, newPass);
                return Ok(member);
            }
            return NotFound();
        }
        [HttpGet("Login")]
        public IActionResult Login(string username, string oldPass)
        {
            var check = _memberRepository.Login(username, oldPass);
            if (check == true)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
