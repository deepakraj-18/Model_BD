using Microsoft.EntityFrameworkCore.Metadata;
using Model_BD.API.Model;
using Model_BD.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_BD.BAL.IService
{
    public interface IModelService
    {
        dynamic GetModelList(int skip, int take, bool showAll);
        dynamic GetModelByAgentId(int agentId);
        void AddModel(ModelDTO agentModel);
        void EditModel(UserDetailModel agentModel, long loginId);
    }
}
