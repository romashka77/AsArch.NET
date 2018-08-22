using System.Net;
using System.Web.Mvc;

namespace AsArch.NET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetIP()
        {
            //return new JsonResult
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = new { ip = Dns.GetHostName() }//new { ID = 123, Name = "Name1" };
            //};
            return Json(new { ip = Dns.GetHostName() }, JsonRequestBehavior.AllowGet);
        }
    }
}