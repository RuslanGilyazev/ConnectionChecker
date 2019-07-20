using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectionChecker.Controllers
{
    public class StaticContentController : Controller
    {
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}