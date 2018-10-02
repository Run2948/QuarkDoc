using Mins.QuarkDoc.BusinessService;
using Mins.QuarkDoc.DataEntities;
using Mins.QuarkDoc.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mins.QuarkDoc.Web.Controllers
{
    public class DocumentController : BasicsAuthentication
    {
        // GET: Document
        public ActionResult Index()
        {
            string applicationId = Request.QueryString["pcode"];
            string doecumentId = Request.QueryString["code"];
            if (applicationId == null && string.IsNullOrEmpty(applicationId))
                return Redirect("../Home");
            if (doecumentId == null && string.IsNullOrEmpty(doecumentId))
                doecumentId = "null";
            else
            {
               
            }
            ApplicationBusinessService applicationBusinessService = new ApplicationBusinessService();
            Application application = applicationBusinessService.FirstOrDefault(new DirectSpecification<Application>(t => t.IsEnabled && t.Id == applicationId));
            if (application == null)
                return Redirect("../Home");
            ViewData["pcode"] = applicationId;
            ViewData["code"] = doecumentId;
            ViewData["projectName"] = application.ProjectName;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult DocumentEdit()
        {
            string applicationId = Request.QueryString["pcode"];
            string doecumentId = Request.QueryString["code"];
            if (applicationId == null && string.IsNullOrEmpty(applicationId))
                return Redirect("../Home");
            if (doecumentId == null && string.IsNullOrEmpty(doecumentId))
                doecumentId = "null";
            ViewData["pcode"] = applicationId;
            ViewData["code"] = doecumentId;
            return View();
        }
        [HttpPost]
        public ActionResult FindData()
        {
            string applicationId = Request["pcode"];
            string doecumentId = Request["code"];
            Documents documents = new Documents();
            object dirResult = null;
            object docResult = null;
            if (applicationId == null || string.IsNullOrEmpty(applicationId))
                return Redirect("../Home");
            IEnumerable<Directories> dirList = null;
            IEnumerable<Documents> docList = null;
            DirectoriesBusinessService directoriesBusinessService = new DirectoriesBusinessService();
            dirList = directoriesBusinessService.FindAll(new DirectSpecification<Directories>(t => t.IsEnabled && t.ApplicationId == applicationId));
            DocumentBusinessService documentBusinessService = new DocumentBusinessService();
            docList = documentBusinessService.FindAll(new DirectSpecification<Documents>(t => t.IsEnabled && t.ApplicationId == applicationId));
            if (docList != null && docList.Count() > 0)
            {
                docResult = docList.Select(t => new { Id = t.Id, Title = t.Title, DirectoryId = t.DirectoryId, ApplicationId = t.ApplicationId });
            }
            if (dirList != null && dirList.Count() > 0)
            {
                dirResult = dirList.Select(t => new { Id = t.Id, Title = t.DirectoryName, DirectoryId = t.DirectoryId });
            }
            return Json(new { directories = dirResult, documents = docResult }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FindDocument()
        {
            string documentId = Request["code"];
            string parentDocumentId = "0";
            DocumentBusinessService documentBusinessService = new DocumentBusinessService();
            Documents documents = documentBusinessService.FirstOrDefault(new DirectSpecification<Documents>(t => t.Id == documentId));
            DirectoriesBusinessService directoriesBusinessService = new DirectoriesBusinessService();
            Directories directories = directoriesBusinessService.FirstOrDefault(new DirectSpecification<Directories>(t => t.IsEnabled && t.Id == documents.DirectoryId));
            if (directories != null)
                parentDocumentId = directories.DirectoryId;
            return Json(new { documents = documents, parentDocumentId = parentDocumentId }, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public ActionResult SaveDocument()
        {
            bool result = false;
            try
            {
                string code = Request["code"];
                string pcode = Request["pcode"];
                string context = Request["context"];
                string title = Request["title"];
                string directoryId = Request["directoryId"];
                string sortStr = Request["sort"];
                DocumentBusinessService documentBusinessService = new DocumentBusinessService();
                Documents documents = new Documents();
                documents.DirectoryId = directoryId;
                documents.Title = title;
                documents.Document = context;
                documents.ApplicationId = pcode;
                int sort = 99;
                if (sortStr == null || !int.TryParse(sortStr, out sort))
                    sort = 99;
                documents.Sort = sort;
                if (code == null || code == "null")
                {
                    documents.Id = Guid.NewGuid().ToString("N");
                    documents.IsEnabled = true;
                    documents.CreateTime = DateTime.Now;
                    documentBusinessService.Insert(documents);
                }
                else
                {
                    documents.Id = code;
                    documentBusinessService.UpdateField(documents, new[] { "Title", "Sort", "DirectoryId", "Document" });
                }
                ViewData["pcode"] = documents.ApplicationId;
                ViewData["code"] = documents.Id;
                result = true;
            }
            catch (Exception ex)
            {

                result = false;
            }
            return Content(result.ToString());
        }
    }
}