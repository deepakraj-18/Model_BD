using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_BD.BAL.IService;
using Model_BD.BAL.Models;
using Model_BD.DAL.Models;

namespace Model_BD.API.Controller
{

    public class UserController(spamanagementContext spamanagementContext, IUserService userService) : BaseController
    {
        [HttpGet]
        public ActionResult GetUserList(int skip, int take, int roleId)
        {
            try
            {
                var result = userService.GetUserList(skip, take, roleId);
                if (result is null)
                    return BadRequest("User List is empty");
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
         }

        [HttpGet]
        public ActionResult GetUser(long id)
        {
            try
            {
                UserDetail userDetail = spamanagementContext.UserDetails.Where(c => c.Id == id).FirstOrDefault();
                if (userDetail == null)
                    return NotFound("User not found");
                return Ok(userDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult AddUser(AddUserDetailModel addUserDetailModel)
        {
            try
            {

                var result = userService.AddUser(addUserDetailModel);
                if (result is string)
                    return BadRequest(result);
                return Ok($"User Added Succesfully{result.Id}");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPatch]
        public ActionResult UpdateUser(EditUserDetailModel editUserDetailModel)
        {
            try
            {
                var userDetail = spamanagementContext.UserDetails.Where(c => c.Id == editUserDetailModel.Id).FirstOrDefault();
                if (userDetail != null)
                {
                    userDetail.FirstName = editUserDetailModel.FirstName;
                    userDetail.LastName = editUserDetailModel.LastName;
                    userDetail.Address = editUserDetailModel.Address;
                    userDetail.Email = editUserDetailModel.Email;
                    userDetail.MobileNo = editUserDetailModel.MobileNo;
                    //userDetail.ModifiedBy=
                    userDetail.ModifiedDate = DateTime.Now;
                    userDetail.IsDeleted = editUserDetailModel.IsDeleted;
                    spamanagementContext.SaveChanges();
                    return Ok($"User updated successfully {userDetail?.Id}");
                }
                return NotFound("User invalid");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
