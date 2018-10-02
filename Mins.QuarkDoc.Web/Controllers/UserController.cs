using Mins.QuarkDoc.BusinessService;
using Mins.QuarkDoc.DataEntities;
using Mins.QuarkDoc.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Mins.QuarkDoc.Web.Controllers
{
    public class UserController : BasicsAuthentication
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public string FindUsers()
        {
            UserBusinessService userBusinessService = new UserBusinessService();
            IEnumerable<User> userList = userBusinessService.FindAll();
            return SerializeHelper.JSONSerialize(new { entities = userList });
        }

        public ActionResult EditUser()
        {
            string type = Request["type"];
            bool result = false;
            try
            {
                UserBusinessService userBusinessService = new UserBusinessService();
                if (type == "new")
                {
                    string isAdminStr = Request["isAdmin"];
                    bool isAdmin = true;
                    bool.TryParse(isAdminStr, out isAdmin);
                    string isEnabledStr = Request["isEnabled"];
                    bool isEnabled = true;
                    bool.TryParse(isEnabledStr, out isEnabled);
                    string userName = Request["userName"];
                    string email = Request["email"];
                    string password = Request["password"];
                    userBusinessService.Insert(new User { Id = Guid.NewGuid().ToString("N"), UserName = userName, IsAdmin = isAdmin, IsEnabled = isEnabled, Password = Encryption.MD5Encrypt64(password).ToLower(), CreateTime = DateTime.Now, Email = email });
                }
                else
                {
                    string isAdminStr = Request["isAdmin"];
                    bool isAdmin = true;
                    bool.TryParse(isAdminStr, out isAdmin);
                    string isEnabledStr = Request["isEnabled"];
                    bool isEnabled = true;
                    bool.TryParse(isEnabledStr, out isEnabled);
                    string userName = Request["userName"];
                    string email = Request["email"];
                    string id = Request["code"];
                    userBusinessService.UpdateField(new User { Id = id, UserName = userName, IsAdmin = isAdmin, IsEnabled = isEnabled, Email = email }, new[] { "UserName", "IsAdmin", "IsEnabled", "Email" });
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Content(result.ToString());
        }

        public ActionResult ResetPassword()
        {
            bool result = false;
            try
            {
                string password = Request["password"];
                string id = Request["code"];
                UserBusinessService userBusinessService = new UserBusinessService();
                userBusinessService.UpdateField(new User
                {
                    Id = id,
                    Password = Encryption.MD5Encrypt64(password).ToLower()
                }, new[] { "Password" });
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return Content(result.ToString());
        }
    }
}