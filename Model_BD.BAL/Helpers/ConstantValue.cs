using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.Helpers
{
    public class ConstantValue
    {
        public const string Password_EncryptionKey = "asdas9f6732ygfawh89";
        public const string Role_Admin = "ADMIN";
        public const string Role_Agent = "AGENT";
        public const string Role_Model = "MODEL";

        public const string Task_Status_Assigned = "ASSIGNED";
        public const string Task_Status_Completed = "COMPLETED";

        public class AppMessage
        {
            public const string IncorrectUserName = "Incorrect UserName";
            public const string IncorrectPassword = "Incorrect Password";
        }
    }
}
