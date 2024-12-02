using Microsoft.AspNetCore.Mvc;
using Model_BD.BAL.Service;
using Model_BD.BAL.IService;
using Model_BD.DAL.Models;
using Model_BD.BAL.Models;
using Model_BD.API.Model;
using Model_BD.BAL.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Model_BD.API.Controller
{

    public class AgentController(IAgentService agentService,spamanagementContext _spamanagementContext) : BaseController
    {
        [HttpGet]
        public IActionResult AgentList(int skip,int take,bool showAll)
        {
            try
            {
                var res = agentService.GetAgentList(skip,take,showAll);
                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddAgent(AgentModel agentModel)
        {
            try
            {
                var agentRoleId = _spamanagementContext.RoleMasters.Where(c => c.Label == ConstantValue.Role_Agent).FirstOrDefault().Id;
                UserDetailModel addAgent = new UserDetailModel()
                {
                    FirstName = agentModel.FirstName,
                    LastName = agentModel.LastName,
                    Email = agentModel.Email,
                    Address= agentModel.Address,
                    MobileNo= agentModel.MobileNo,
                    RoleId= agentRoleId,
                };
                agentService.AddAgent(addAgent, LongInUserId);

                return Ok("Successfully Added");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateAgent(EditUserDetailModel agentModel)
        {
            try
            {
                agentService.EditAgent(agentModel);
                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
