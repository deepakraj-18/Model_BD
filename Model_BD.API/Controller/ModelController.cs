using Microsoft.AspNetCore.Mvc;
using Model_BD.BAL.IService;
using Model_BD.BAL.Models;

namespace Model_BD.API.Controller
{
    
    public class ModelController : BaseController
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        public IActionResult ModelList()
        {
            try
            {
                var res = _modelService.GetModelList();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddModel(UserDetailModel agentModel)
        {
            try
            {
                _modelService.AddModel(agentModel, LongInUserId);

                return Ok("Successfully Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult UpdateModel(UserDetailModel agentModel)
        {
            try
            {
                _modelService.EditModel(agentModel, LongInUserId);

                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
