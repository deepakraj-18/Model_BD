using Model_BD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.IService
{
    public interface IStatusService
    {
        List<TaskStatusMaster> GetStatus();
        List<TaskStatusMaster> GetStatusById();
        List<TaskStatusMaster> GetStatusByLabel(string Label);
    }
}
