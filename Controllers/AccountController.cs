using Microsoft.AspNetCore.Mvc;
using TasksWebApi.Interfaces;
using TasksWebApi.Models;

namespace TasksWebApi.Controllers
{
   
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
       

        public AccountController(IAccountService accountService)
        {
           _accountService = accountService;
           
        }
        [HttpPost("register")]
        public  ActionResult RegisterUser([FromBody]RegisterUserDto userDto)
        {
            
            _accountService.RegisterUser(userDto);
           
                return Ok();
        }

        [HttpPost("{login}")]
        public ActionResult Login([FromBody]LoginDto loginDto)
        {
            string token = _accountService.GenerateJwt(loginDto);
            return Ok(token);
        }
    } 
}
