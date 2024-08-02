using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Repository.IRepository;

namespace NationalParkAPI_01.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody]User user)
        {
           if(ModelState.IsValid)
            {
                var isUniqueUser = _userRepository.IsUniqueUser(user.UserName);
                if (!isUniqueUser) return BadRequest("User in use");
                var userinfo  = _userRepository.Register(user.UserName, user.Password);
                if (userinfo == null) return BadRequest();              
            }
           return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserVM userVM)  
        {
            var user = _userRepository.Authenticate(userVM.UserName,userVM.Password);
            if (user == null) return BadRequest("Wrong User/Password");
            return Ok(user);
        }
    }
}
