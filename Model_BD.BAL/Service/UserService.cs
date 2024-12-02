using Model_BD.BAL.Helpers;
using Model_BD.BAL.IService;
using Model_BD.BAL.Models;
using Model_BD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.Service
{
    public class UserService(spamanagementContext spamanagementContext) : IUserService
    {


        public dynamic? AddUser(AddUserDetailModel addUserDetailModel)
        {
            try
            {
                var result = GetUserByMailOrMobileNumber(addUserDetailModel.Email, addUserDetailModel.MobileNo);
                if (result is not null)
                {
                    return result;
                }
                UserDetail userDetail = new UserDetail()
                {
                    FirstName = addUserDetailModel.FirstName,
                    LastName = addUserDetailModel.LastName,
                    Address = addUserDetailModel.Address,
                    MobileNo = addUserDetailModel.MobileNo,
                    Email = addUserDetailModel.Email,
                    RoleId = addUserDetailModel.RoleId,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };
                spamanagementContext.UserDetails.Add(userDetail).Context.SaveChanges();
                return userDetail;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public dynamic IsValidUser(AuthenticationModel authenticationModel)
        {
            if (authenticationModel != null)
                return VerifyUser(authenticationModel.Username, authenticationModel.Password);
            return false;
        }


        public dynamic? GetUserByMailOrMobileNumber(string email, string mobileNumber)
        {
            try
            {
                var result = spamanagementContext.UserDetails.Where(c => c.MobileNo == mobileNumber || c.Email == email).FirstOrDefault();
                if (result == null)
                    return null;
                return "User mobile or email already exists";
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<string> GetRoles(long userId)
        {
            return spamanagementContext.UserDetails.Where(c => c.Id == userId).Select(u => u.Role.Label).ToList();
        }
        private dynamic VerifyUser(string username, string password)
        {
            var user = spamanagementContext.UserDetails.FirstOrDefault(u => u.Username == username && u.IsDeleted == false);
            if (user == null)
                return ConstantValue.AppMessage.IncorrectUserName;
            //if (user != null && !email.Contains('@') && email != user.Email)
            //    return ConstantValue.AppMessage.IncorrectUserName;
            if (user.Password != password)
                return ConstantValue.AppMessage.IncorrectPassword;
            return user;

        }

        public dynamic? GetUserList(int skip, int take, int roleId)
        {
            try
            {
                List<UserDetail> result = spamanagementContext.UserDetails.ToList();
                if (result is null)
                    return null;
                return new
                {
                    count = result.Count,
                    records = result.Skip(skip).Take(take).ToList()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }


        public dynamic? GetAdminUser(LoginAdminModel loginAdminModel)
        {
            try
            {
                UserDetail result = spamanagementContext.UserDetails.Where(c => c.MobileNo == loginAdminModel.EmailAndMobile || c.Email == loginAdminModel
                .EmailAndMobile).FirstOrDefault();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception)
            {


                throw;
            }
        }
    }
}
