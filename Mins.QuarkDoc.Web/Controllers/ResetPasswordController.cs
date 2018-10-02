using System.Web.Mvc;
using Mins.QuarkDoc.BusinessService;
using Mins.QuarkDoc.DataEntities;
using Mins.QuarkDoc.Framework;
namespace Mins.QuarkDoc.Web.Controllers
{
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reset()
        {
            bool result = false;
            try
            {
                var password = Request["password"];
                var newPassword = Request["newPassword"];
                var email = Request["email"];
                UserBusinessService userBusinessService = new UserBusinessService();
                password = Encryption.MD5Encrypt64(password).ToLower();
                newPassword = Encryption.MD5Encrypt64(newPassword).ToLower();
                User user = userBusinessService.Login(email, password);
                if (user == null)
                    result = false;
                else
                {
                    user.Password = newPassword;
                    userBusinessService.UpdateField(user, new[] { "Password" });
                    result = true;
                }
            }
            catch (System.Exception)
            {

                result = false;
            }
            return Content(result.ToString());
        }
    }
}