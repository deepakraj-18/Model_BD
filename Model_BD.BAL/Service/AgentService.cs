using Microsoft.EntityFrameworkCore;
using Model_BD.DAL.Models;
using Model_BD.BAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_BD.BAL.IService;
using Model_BD.BAL.Models;

namespace Model_BD.BAL.Service
{
    public class AgentService : IAgentService
    {
        private readonly spamanagementContext _spamanagementContext;

        public AgentService(spamanagementContext spamanagementContext)
        {
            _spamanagementContext = spamanagementContext;
        }

        public dynamic GetAgentList()
        {
            long roleId = _spamanagementContext.RoleMasters.First(r => r.Label == ConstantValue.Role_Agent).Id;

            var agents = _spamanagementContext.UserDetails.Where(ag => ag.RoleId == roleId && ag.IsDeleted == false);
            var data = agents.Select(s => new
            {
                s.Id,
                s.FirstName,
                s.Address,
                s.Email,
                s.Password,
                s.MobileNo,
                s.RoleId,
                s.Role,
            });
            return data;
        }

        public void AddAgent( UserDetailModel agentModel, long loginId)
        {
            UserDetail user = new UserDetail()
            {
                FirstName = agentModel.FirstName,
                LastName = agentModel.LastName,
                Address = agentModel.Address,
                Email = agentModel.Email,
                Password = agentModel.Password,
                RoleId = agentModel.RoleId,

                CreatedBy = loginId,
                CreatedDate = DateTime.Now
            };

            _spamanagementContext.UserDetails.Add(user);
            _spamanagementContext.SaveChanges();
        }

        public void EditAgent( UserDetailModel agentModel, long loginId )
        {
            var agent = _spamanagementContext.UserDetails.FirstOrDefault(a => a.Id == loginId && a.IsDeleted == false);
            if (agent == null)
                throw new Exception("Agent Not Found");

            agent.FirstName = agentModel.FirstName;
            agent.LastName = agentModel.LastName;
            agent.Address = agentModel.Address;
            agent.Email = agentModel.Email;
            agent.Password = agentModel.Password;
            agent.RoleId = agentModel.RoleId;

            agent.ModifiedBy = loginId;
            agent.ModifiedDate = DateTime.Now;

            _spamanagementContext.SaveChanges();
        }
    }
}
