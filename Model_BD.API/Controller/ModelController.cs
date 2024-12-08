using Microsoft.AspNetCore.Mvc;
using Model_BD.API.Model;
using Model_BD.BAL.IService;


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
        public IActionResult ModelList(int skip, int take, bool showAll)
        {
            try
            {
                var res = _modelService.GetModelList(skip, take, showAll);

                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddModel(ControllerModel agentModel)
        {
            try
            {
                ModelDTO addModel = new ModelDTO()
                {
                    FirstName = agentModel.FirstName,
                    LastName = agentModel.LastName,
                    Address = agentModel.Address,
                    Email = agentModel.Email,
                    MobileNo = agentModel.MobileNo
                };
                _modelService.AddModel(addModel);
                return Ok("Successfully Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetModelByAgentId(int agentid)
        {
            try
            {
                var result = _modelService.GetModelByAgentId(agentid);
                if (result != null)
                    return Ok(result);
                else
                    return BadRequest("The list is empty");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[HttpPatch]
        //public IActionResult UpdateModel(UserDetailModel agentModel)
        //{
        //    try
        //    {
        //        _modelService.EditModel(agentModel, LongInUserId);

        //        return Ok("Successfully Updated");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
