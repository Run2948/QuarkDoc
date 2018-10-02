using System.Web.Mvc;

namespace Mins.QuarkDoc.Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult JsonFormatting()
        {
            return View();
        }
        public ActionResult JsonToURL()
        {
            return View();
        }
        public ActionResult HttpSimulate()
        {
            return View();
        }
    }
}