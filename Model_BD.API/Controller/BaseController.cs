using Microsoft.AspNetCore.Mvc;

namespace Model_BD.API.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        public long LongInUserId
        {
            get
            {
                try
                {
                    string strValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                    return string.IsNullOrEmpty(strValue) ? 0 : Convert.ToInt64(strValue);
                }
                catch (Exception)
                {

                    return 0;
                }
            }
        }
    }
}
