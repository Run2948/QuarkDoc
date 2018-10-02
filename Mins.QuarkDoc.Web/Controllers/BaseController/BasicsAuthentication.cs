using Mins.QuarkDoc.DataEntities;
using System;
using System.Web.Mvc;
namespace Mins.QuarkDoc.Web.Controllers
{
    public class BasicsAuthentication : Controller
    {
        public User Os_User { get; set; }
        public bool IsAdmin { get; set; } = false;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {
                object userObj = Session["OS_USER"];
                if (userObj != null)
                {
                    this.Os_User = userObj as User;
                    this.IsAdmin = Os_User.IsAdmin;
                    ViewData["OS_ISADMIN"] = Os_User.IsAdmin;
                    ViewData["OS_USERNAME"] = Os_User.IsAdmin;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Login");
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("/Login");
            }

        }
    }
}