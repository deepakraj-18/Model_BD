using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_BD.API.Helper;
using Model_BD.BAL.IService;
using Model_BD.BAL.Models;

namespace Model_BD.API.Controller
{
  
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly JWT _jwt;

        public AuthenticationController(IUserService userService, JWT cryptography)
        {
            _userService = userService;
            _jwt = cryptography;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AuthenticationModel authenticationModel)
        {
            var userDetail = _userService.IsValidUser(authenticationModel);
            if(userDetail is string)
                return BadRequest(userDetail);

            List<string> userRoles = _userService.GetRoles(userDetail.Id);

            string token = _jwt.GenerateUserToken(new CommenModel
            {
                Email = userDetail.Email,
                Role = string.Join(",", userRoles),
            }, true);

            return Ok(new
            {
                token,
                roles = userRoles,
                userDetail.Id,
                userDetail.FirstName,
                userDetail.LastName,
                userDetail.Email,
                userDetail.MobileNo,
                userDetail.Username
            });
        }
    }
}
