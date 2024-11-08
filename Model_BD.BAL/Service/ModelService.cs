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
    public class ModelService : IModelService
    {
        private readonly spamanagementContext _spamanagementContext;

        public ModelService(spamanagementContext spamanagementContext)
        {
            _spamanagementContext = spamanagementContext;
        }

        public dynamic GetModelList()
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

        public void AddModel(UserDetailModel agentModel, long loginId)
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

        public void EditModel(UserDetailModel agentModel, long loginId)
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

