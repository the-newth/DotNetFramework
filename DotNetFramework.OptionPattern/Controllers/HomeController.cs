using DotNetFramework.OptionPattern.Models;
using DotNetFramework.OptionPattern.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetFramework.OptionPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly SecretOptions secret;

        public HomeController(IOptions<SecretOptions> secret)
        {
            this.secret = secret.Value;
        }

        public ActionResult Index()
        {
            var Name = secret.secretName;
            var Key = secret.secretKey;

            //using info

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
    }
}