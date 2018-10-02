using Mins.QuarkDoc.BusinessService;
using Mins.QuarkDoc.DataEntities;
using Mins.QuarkDoc.Framework;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Mins.QuarkDoc.Web.Controllers
{
    public class HomeController : BasicsAuthentication
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string FindApplication()
        {
            IEnumerable<Application> entityList = null;
            int pageCount = 1;
            int totalCount = 0;
            try
            {
                string page = Request["pageIndex"];
                int pageIndex = int.Parse(page);
                ApplicationBusinessService applicationBusinessService = new ApplicationBusinessService();
                Expression<Func<Application, bool>> expression = t => t.IsEnabled;
                 totalCount = applicationBusinessService.GetPagedCount(new DirectSpecification<Application>(expression));
                int pageSize = 4;
                if (totalCount > 0)
                {
                    pageCount = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                    Expression<Func<Application, DateTime>> order = t => t.CreateTime;
                    entityList = applicationBusinessService.GetPagedElements<DateTime>(pageIndex, new DirectSpecification<Application>(expression), order, false, pageSize);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex);
            }
            return SerializeHelper.JSONSerialize(new { entities = entityList, pageCount = pageCount,totalCount= totalCount });
        }
        [HttpPost]
        public ActionResult NewApplication()
        {
            bool result;
            try
            {
                string projectName = Request["projectName"];
                string description = Request["description"];
                if (projectName.Trim() == "")
                    projectName = "undefound";
                if (description.Trim() == "")
                    description = "undefound";
                ApplicationBusinessService applicationBusinessService = new ApplicationBusinessService();
                applicationBusinessService.Insert(new Application() { Id = Guid.NewGuid().ToString("N").ToLower(), Description = description, ProjectName = projectName, UserId = Os_User.Id });
                result = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(ex);
                result = false;
            }
            return Content(result.ToString());
        }
    }
}