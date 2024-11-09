using Model_BD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.IService
{
    public interface ITaskService
    {
        long Add(TaskList taskList);
        long Update(TaskList taskList);
        List<TaskList> List(int skip, int take);
        dynamic? GetTaskById(long id);
    }
}
