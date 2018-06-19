using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult MenCategory()
        {
            var men = from table in db.DanhMucs where table.Ten.Contains("Nam") select table;
            List < DanhMuc > catlist = men.Select(n => n).ToList();
            return PartialView(catlist);
        }

        [ChildActionOnly]
        public ActionResult WomenCategory()
        {
            var women = from table in db.DanhMucs where table.Ten.Contains("Nữ") select table;
            List<DanhMuc> catlist = women.Select(n => n).ToList();
            return PartialView(catlist);
        }

        public ActionResult ListPro(int idDM)
        {
            List<SanPham> sp = (from table in db.SanPhams where table.MaDM == idDM select table).ToList();
            return View(sp);
        }

        public ActionResult ListProHeader(string idheader)
        {
            List<SanPham> sp = (from table in db.SanPhams where table.GioiTinh.Contains(idheader) select table).ToList();
            return View(sp);
        }

        public ActionResult ListAll()
        {
            List<SanPham> sp = (from table in db.SanPhams select table).ToList();
            return View(sp);
        }

    }
}