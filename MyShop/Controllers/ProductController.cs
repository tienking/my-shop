using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        public ViewResult Detail(int id)
        {
            Model1 db = new Model1();
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP==id);
            return View(sp);
        }
    }
}