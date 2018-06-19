using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Model1 db = new Model1();
        public ActionResult Index()
        {
            List<SanPham> listsp = db.SanPhams.Select(n => n).ToList();
            return View(listsp);
        }
    }
}