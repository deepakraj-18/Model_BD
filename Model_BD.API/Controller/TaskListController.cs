﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_BD.BAL.IService;

namespace Model_BD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskListController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public ActionResult Add()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
