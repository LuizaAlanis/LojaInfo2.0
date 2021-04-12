using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LojaInfo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }
    }
}