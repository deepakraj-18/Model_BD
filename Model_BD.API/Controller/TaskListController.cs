using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_BD.API.Model;
using Model_BD.BAL.IService;
using Model_BD.DAL.Models;

namespace Model_BD.API.Controller
{

    public class TaskListController(ITaskService taskService) : BaseController
    {
        [HttpPost]
        public ActionResult Add(AddModelTask addTask)
        {
            try
            {
                //var result = taskService.Add();
                return Ok();
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
                var result = taskService.Update(taskList);
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
                var result = taskService.GetTaskById(id);
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
                var result = taskService.List(skip, take);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
