using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_BD.API.Model;
using Model_BD.BAL.Helpers;
using Model_BD.BAL.IService;
using Model_BD.DAL.Models;

namespace Model_BD.API.Controller
{

    public class TaskListController(ITaskService _taskService,IStatusService _statusService) : BaseController
    {
        [HttpPost]
        public ActionResult Add(AddAgentTask addTask)
        {
            try
            {
                var status = _statusService.GetStatusByLabel(ConstantValue.Task_Status_Assigned);
                TaskList newTask = new TaskList()
                {
                    AgentId = addTask?.AgentId,
                    ModelId = addTask?.ModelId,
                    GuestFirstName = addTask?.GuestFirstName,
                    GuestPhoneNo = addTask?.GuestPhoneNo,
                    AmountFixed = addTask?.AmountFixed,
                    AdvanceReceived = addTask?.AdvanceReceived,
                    City = addTask?.City,
                    DateAndTime = System.DateTime.Now,
                    StatusId = status?.FirstOrDefault()?.Id
                };
                var result = _taskService.Add(newTask);
                return Ok($"Task added succesfully {result}  ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]

        public ActionResult Update(UpdateModelTask updateModelTask)
        {
            try
            {
                TaskList taskList = new TaskList()
                {
                    
                };
                var result = _taskService.Update(taskList);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult TaskById(long id)
        {
            try
            {
                var result = _taskService.GetTaskById(id);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult GetTaskList(int skip, int take)
        {
            try
            {
                var list = _taskService.List(skip, take);
                var result = new
                {
                    Count = list.Count(),
                    Data = list
                };
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
