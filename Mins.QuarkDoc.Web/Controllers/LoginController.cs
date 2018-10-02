using Mins.QuarkDoc.BusinessService;
using Mins.QuarkDoc.DataEntities;
using Mins.QuarkDoc.Framework;
using System;
using System.Web.Mvc;

namespace Mins.QuarkDoc.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            string result = string.Empty;
            try
            {
                string email = Request["email"].ToLower();
                string password = Request["password"];
                UserBusinessService userBusinessService = new UserBusinessService();
                password = Encryption.MD5Encrypt64(password).ToLower();
                User user = userBusinessService.Login(email, password);
                if (user == null)
                    result = "no";
                else
                {
                    Session["OS_USER"] = user;
                    result = "ok";
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex);
                result = "no";
            }
            return Content(result);
        }
    }
}