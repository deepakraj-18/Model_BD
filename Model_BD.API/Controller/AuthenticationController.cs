using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_BD.BAL.Models;
using Model_BD.BAL.Service;

namespace Model_BD.API.Controller
{

    public class AuthenticationController(UserService userService) : BaseController
    {

        [HttpPost]
        public ActionResult AdminLogin(LoginAdminModel loginAdminModel)
        {
            try
            {
                var result = userService.GetAdminUser(loginAdminModel);
                if (result == null)
                    return BadRequest("Invalid User");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
