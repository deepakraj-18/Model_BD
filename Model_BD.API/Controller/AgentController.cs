﻿using Microsoft.AspNetCore.Mvc;
using Model_BD.BAL.Service;
using Model_BD.BAL.IService;
using Model_BD.DAL.Models;
using Model_BD.BAL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Model_BD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : BaseController
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        public IActionResult AgentList()
        {
            try
            {
                var res = _agentService.GetAgentList();

                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddAgent(UserDetailModel agentModel)
        {
            try
            {
                _agentService.AddAgent(agentModel, LongInUserId);

                return Ok("Successfully Added");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult UpdateAgent(UserDetailModel agentModel)
        {
            try
            {
                _agentService.EditAgent(agentModel, LongInUserId);

                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
