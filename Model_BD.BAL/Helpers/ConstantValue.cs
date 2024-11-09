using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.Helpers
{
    public class ConstantValue
    {
        public const string Role_Admin = "ADMIN";
        public const string Role_Agent = "AGENT";
        public const string Role_Model = "MODEL";

        public class AppMessage
        {
            public const string IncorrectUserName = "Incorrect UserName";
            public const string IncorrectPassword = "Incorrect Password";
        }
    }
}
