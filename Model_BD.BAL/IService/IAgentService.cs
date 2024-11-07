using Model_BD.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.IService
{
    public interface IAgentService
    {
        dynamic GetAgentList();

        void AddAgent(UserDetailModel agentModel, long loginId);

        void EditAgent(UserDetailModel agentModel, long loginId);
    }
}
