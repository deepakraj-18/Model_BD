using Model_BD.BAL.Models;
using Model_BD.BAL.Helpers;
using Model_BD.BAL.IService;
using Model_BD.DAL.Models;
using Model_BD.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_BD.API.Model;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Model_BD.BAL.Service
{
    public class ModelService : IModelService
    {
        private readonly spamanagementContext _spamanagementContext;
        private readonly Cryptography _cryptography;

        public ModelService(spamanagementContext spamanagementContext, Cryptography cryptography)
        {
            _spamanagementContext = spamanagementContext;
            _cryptography = cryptography;
        }

        public dynamic GetModelList(int skip, int take, bool showAll)
        {
            long roleId = _spamanagementContext.RoleMasters.First(r => r.Label == ConstantValue.Role_Model).Id;

            var models = _spamanagementContext.UserDetails.Where(ag => ag.RoleId == roleId && ag.IsDeleted == false).Select(s => new
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
            if (showAll && models.Any())
            {
                return models.ToList();
            }
            else if (models.Any())
            {
                return new
                {
                    Count = models.Count(),
                    Data = models.Skip(skip).Take(take).ToList()
                };
            }
            else
                return null;
        }

        public void AddModel(ModelDTO agentModel)
        {
            string password = _cryptography.PasswordGenerator(8, true, true, true);
            long roleId = _spamanagementContext.RoleMasters.First(r => r.Label == ConstantValue.Role_Model).Id;
            UserDetail addUser = new UserDetail()
            {
                FirstName = agentModel.FirstName,
                LastName = agentModel.LastName,
                Address = agentModel.Address,
                Email = agentModel.Email,
                Password = password,
                RoleId = roleId,
                MobileNo = agentModel.MobileNo,
                Username = GenerateUsername(agentModel.FirstName)
            };
            _spamanagementContext.UserDetails.Add(addUser);
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
        private string GenerateUsername(string name)
        {
            var userCount = _spamanagementContext.UserDetails.Count();
            return "MOD" + name + userCount;
        }

    }
}

