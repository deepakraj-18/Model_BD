using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_BD.API.Helper;
using Model_BD.BAL.Helpers;
using Model_BD.BAL.IService;
using Model_BD.BAL.Models;
using Cryptography = Model_BD.API.Helper.Cryptography;

namespace Model_BD.API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly Cryptography _cryptography;

        public AuthenticationController(IUserService userService, Cryptography cryptography)
        {
            _userService = userService;
            _cryptography = cryptography;
        }

        [HttpPost]
        public IActionResult UserSignIn([FromBody] AuthenticationModel authenticationModel)
        {
            var userDetail = _userService.IsValidUser(authenticationModel);
            if(userDetail is string)
                return BadRequest(userDetail);

            List<string> userRoles = _userService.GetRoles(userDetail.Id);

            string token = _cryptography.GenerateUserToken(new CommenModel
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
                userDetail.MobileNo
            });
        }
    }
}
