using Model_BD.BAL.IService;
using Model_BD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.Service
{
    public class TaskService(spamanagementContext spamanagementContext) : ITaskService
    {
        
        public long Add(TaskList taskList)
        {
            try
            {
                spamanagementContext.TaskLists.Add(taskList);
                spamanagementContext.SaveChanges();
                return taskList.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public long Update(TaskList taskList)
        {
            try
            {
                var result = spamanagementContext.TaskLists.Where(c => c.Id == taskList.Id).FirstOrDefault();
                result.GuestFirstName = taskList.GuestFirstName;
                spamanagementContext.SaveChanges();
                return result.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TaskList> List(int skip, int take)
        {
            try
            {
                var result = spamanagementContext.TaskLists.Where(c => c.IsDeleted == false).Skip(skip).Take(take).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public dynamic? GetTaskById(long id)
        {
            try
            {
                var result = spamanagementContext.TaskLists.Where(c => c.Id == id).FirstOrDefault();
                if (result == null)
                    return null;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
