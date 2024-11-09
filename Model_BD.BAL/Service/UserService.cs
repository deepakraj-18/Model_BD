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
    public class UserService : IUserService
    {
        private readonly spamanagementContext _spamanagementContext;

        public UserService(spamanagementContext spamanagementContext)
        {
            _spamanagementContext = spamanagementContext;
        }

        public dynamic IsValidUser(AuthenticationModel authenticationModel)
        {
            if (authenticationModel != null) 
                return VerifyUser(authenticationModel.Email, authenticationModel.Password);

            return false;
        }

        public List<string> GetRoles(long userId)
        {
            return _spamanagementContext.UserDetails.Where(c => c.Id == userId).Select(u => u.Role.Name).ToList();
        }

        private dynamic VerifyUser(string email, string password)
        {
            var user = _spamanagementContext.UserDetails.FirstOrDefault(u => u.Email == email && u.IsDeleted == false);

            if (user == null)
                return ConstantValue.AppMessage.IncorrectUserName;

            if (user != null && !email.Contains('@') && email != user.Email)
                return ConstantValue.AppMessage.IncorrectUserName;

            if (user.Password != password)
                return ConstantValue.AppMessage.IncorrectPassword;

            return user;
        }

        
    }
}
