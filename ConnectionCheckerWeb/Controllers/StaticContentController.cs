using System.Web.Mvc;

namespace ConnectionChecker.Controllers
{
    /// <summary>
    /// The static content controller.
    /// </summary>
    public class StaticContentController : Controller
    {
        /// <summary>
        /// The page not found.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult PageNotFound()
        {
            return View();
        }
    }
}