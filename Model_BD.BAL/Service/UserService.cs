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
    public class UserService : IUserService
    {
        private readonly spamanagementContext _spamanagementContext;



        public dynamic? AddUser(AddUserDetailModel addUserDetailModel)
        public UserService(spamanagementContext spamanagementContext)
        {
            try
            {
                var result = GetUserByMailOrMobileNumber(addUserDetailModel.Email, addUserDetailModel.MobileNo);
                if (result is not null)
                {
                    return result;
            _spamanagementContext = spamanagementContext;
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
        public dynamic IsValidUser(AuthenticationModel authenticationModel)
            {
            if (authenticationModel != null) 
                return VerifyUser(authenticationModel.Email, authenticationModel.Password);

                throw;
            return false;
            }
        }


        public dynamic? GetUserByMailOrMobileNumber(string email, string mobileNumber)
        public List<string> GetRoles(long userId)
        {
            try
            {
                var result = spamanagementContext.UserDetails.Where(c => c.MobileNo == mobileNumber || c.Email == email).FirstOrDefault();
                if (result == null)
                    return null;
                return "User mobile or email already exists";
            return _spamanagementContext.UserDetails.Where(c => c.Id == userId).Select(u => u.Role.Name).ToList();
            }
            catch (Exception)

        private dynamic VerifyUser(string email, string password)
            {
                return null;
            }
        }
            var user = _spamanagementContext.UserDetails.FirstOrDefault(u => u.Email == email && u.IsDeleted == false);

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
            if (user == null)
                return ConstantValue.AppMessage.IncorrectUserName;

                return null;
            }
        }
            if (user != null && !email.Contains('@') && email != user.Email)
                return ConstantValue.AppMessage.IncorrectUserName;

            if (user.Password != password)
                return ConstantValue.AppMessage.IncorrectPassword;

        public dynamic? GetAdminUser(LoginAdminModel loginAdminModel)
        {
            try
            {
                UserDetail result = spamanagementContext.UserDetails.Where(c => c.MobileNo == loginAdminModel.EmailAndMobile || c.Email == loginAdminModel
                .EmailAndMobile).FirstOrDefault();
                if (result == null)
                    return null;
                return result;
            return user;
            }
            catch (Exception)
            {


                throw;
            }
        }
    }
}
