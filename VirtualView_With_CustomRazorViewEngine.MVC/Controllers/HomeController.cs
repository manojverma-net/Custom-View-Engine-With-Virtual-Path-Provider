using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualView_With_CustomRazorViewEngine.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Virtual View Action Method
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVirtualFile()
        {
            return View();
        }

    }
}