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
        private readonly Cryptography _cryptography;

        public AgentService(spamanagementContext spamanagementContext, Cryptography cryptography)
        {
            _spamanagementContext = spamanagementContext;
            _cryptography = cryptography;
        }

        public dynamic GetAgentList(int skip, int take, bool showAll)
        {
            long roleId = _spamanagementContext.RoleMasters.First(r => r.Label == ConstantValue.Role_Agent).Id;
            var agents = _spamanagementContext.UserDetails.Where(ag => ag.RoleId == roleId && ag.IsDeleted == false).Select(s => new
            {
                s.Id,
                s.FirstName,
                s.LastName,
                s.Username,
                s.Address,
                s.Email,
                s.MobileNo,
                s.RoleId,
                RoleLabel = s.Role.Label
            });
            if (showAll && agents.Any())
            {
                return  agents.ToList();
            }
            else if (agents.Any())
            {
                return new
                {
                    Count = agents.Count(),
                    Data = agents.Skip(skip).Take(take).ToList()
                };
            }
            else
                return null;
        }

        public void AddAgent(UserDetailModel agentModel, long loginId)
        {
            string password = _cryptography.PasswordGenerator(8, true, true, true);
            UserDetail user = new UserDetail()
            {
                FirstName = agentModel.FirstName,
                LastName = agentModel.LastName,
                Address = agentModel.Address,
                Email = agentModel.Email,
                Password = password,
                RoleId = agentModel.RoleId,
                Username = GenerateUsername(agentModel.FirstName),
                CreatedBy = loginId,
                CreatedDate = DateTime.Now
            };

            _spamanagementContext.UserDetails.Add(user);
            _spamanagementContext.SaveChanges();
        }

        public void EditAgent(EditUserDetailModel agentModel)
        {
            var agent = _spamanagementContext.UserDetails.FirstOrDefault(a => a.Id == agentModel.Id && a.IsDeleted == false);
            if (agent == null)
                throw new Exception("Agent Not Found");

            agent.FirstName = agentModel.FirstName;
            agent.LastName = agentModel.LastName;
            agent.Address = agentModel.Address;
            agent.Email = agentModel.Email;
            agent.ModifiedDate = DateTime.Now;

            _spamanagementContext.SaveChanges();
        }
        private string GenerateUsername(string name)
        {
            var userCount = _spamanagementContext.UserDetails.Count();
            return "AGT" + name + userCount;
        }
    }
}
