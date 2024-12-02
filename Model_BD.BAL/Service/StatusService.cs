using Model_BD.BAL.IService;
using Model_BD.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.Service
{
    public class StatusService(spamanagementContext _spamanagementContext) : IStatusService
    {
        public List<TaskStatusMaster> GetStatus()
        {
            try
            {
                List<TaskStatusMaster> statusList = _spamanagementContext.TaskStatusMasters.Where(status => status.IsDeleted == false).ToList();
                return statusList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TaskStatusMaster> GetStatusByLabel(string label)
        {
            try
            {
                List<TaskStatusMaster> statusList = _spamanagementContext.TaskStatusMasters.Where(status => status.IsDeleted == false&&status.Label==label 
                ).ToList();
                return statusList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TaskStatusMaster> GetStatusById()
        {
            try
            {
                List<TaskStatusMaster> statusList = _spamanagementContext.TaskStatusMasters.Where(status => status.IsDeleted == false).ToList();
                return statusList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
