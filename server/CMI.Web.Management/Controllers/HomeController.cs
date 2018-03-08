using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace CMI.Web.Management.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var language = "de";

            var baseUrl = Request.ApplicationPath +
                          (!Request?.ApplicationPath?.EndsWith("/") == true ? "/" : string.Empty);
            if (!Request.Url.AbsolutePath.EndsWith(baseUrl))
                return Redirect(new Uri(baseUrl + Request.Url.Query, UriKind.Relative).ToString());

            var formatting = Formatting.None;

            var title = "Schweizerisches Bundesarchiv BAR";

            ViewBag.PageTitle = title;
            ViewBag.Language = language;
            return View();
        }
    }
}