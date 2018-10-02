using Mins.QuarkDoc.BusinessService;
using Mins.QuarkDoc.DataEntities;
using Mins.QuarkDoc.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace Mins.QuarkDoc.Web.Controllers
{
    public class ApplicationController : BasicsAuthentication
    {
        // GET: Application
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult CatalogEdit()
        {
            string applicationId = Request["pcode"];
            if (applicationId == null && string.IsNullOrEmpty(applicationId))
                return Redirect("../Home");
            ViewData["ApplicationId"] = applicationId;
            return View();
        }
        [HttpPost]
        public string FindDirectories()
        {
            string applicationId = Request["pcode"];
            IEnumerable<Directories> directoriesList = null;
            if (!string.IsNullOrEmpty(applicationId))
            {
                DirectoriesBusinessService directoriesBusinessService = new DirectoriesBusinessService();
                directoriesList = directoriesBusinessService.FindAll(new DirectSpecification<Directories>(t => t.IsEnabled && t.ApplicationId == applicationId));
            }
            return SerializeHelper.JSONSerialize(new { entities = directoriesList });
        }
        [HttpPost]
        public ActionResult EditDirectories()
        {
            bool result;
            try
            {
                string directoryName = Request["directoryName"].ToLower();
                string sortStr = Request["sort"];
                string directoryId = Request["directoryId"];
                string applicationId = Request["pcode"];
                string Id = Request["editId"];
                string type = Request["type"];
                int sort = 99;
                if (!int.TryParse(sortStr, out sort))
                    sort = 99;
                if (type == "new")
                {
                    DirectoriesBusinessService directoriesBusinessService = new DirectoriesBusinessService();
                    directoriesBusinessService.Insert(new Directories() { ApplicationId = applicationId, Id = Guid.NewGuid().ToString("N").ToLower(), DirectoryName = directoryName, Sort = sort, DirectoryId = directoryId, CreateTime = DateTime.Now, IsEnabled = true });
                    result = true;
                }
                else
                {
                    DirectoriesBusinessService directoriesBusinessService = new DirectoriesBusinessService();
                    directoriesBusinessService.UpdateField(new Directories() { ApplicationId = applicationId, Id = Id, DirectoryName = directoryName, Sort = sort, DirectoryId = directoryId }, new[] { "DirectoryName", "Sort", "DirectoryId" });
                    result = true;
                }
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