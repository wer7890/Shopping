using NLog;
using ShoppingWeb.Filters;
using System;
using System.Configuration;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [LoginFilter]
    [ValidationFilter]
    public class BaseController : ApiController
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;     

        /// <summary>
        /// 紀錄前端錯誤
        /// </summary>
        /// <param name="errorDetails"></param>
        [AllowAnonymous]
        [HttpPost]
        [Route("LogClientError")]
        public void LogClientError([FromBody] string[] errorDetails)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                foreach (var error in errorDetails)
                {
                    logger.Error(error);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "前端NLog錯誤");
            }
        }
    }
}