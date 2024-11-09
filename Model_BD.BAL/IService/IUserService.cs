using Model_BD.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.IService
{
    public interface IUserService
    {
        dynamic? GetAdminUser(LoginAdminModel loginAdminModel);
        dynamic? AddUser(AddUserDetailModel addUserDetailModel);
        dynamic? GetUserList(int skip, int take, int roleId);
    }
}
