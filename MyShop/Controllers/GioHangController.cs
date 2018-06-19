using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Models;

namespace MyShop.Controllers
{
    public class GioHangController : Controller
    {
        Model1 db = new Model1();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lsGioHang = Session["GioHang"] as List<GioHang>;
            if(lsGioHang == null)
            {
                //nếu list GioHang == null, tạo list giỏ hàng
                lsGioHang = new List<GioHang>();
                Session["GioHang"] = lsGioHang;

            }
            return lsGioHang;
        }

        public ActionResult AddGioHang(int MaSP,string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lsGioHang = LayGioHang();
            //Kiểm tra SP tồn tại trong Session
            GioHang sanpham = lsGioHang.Find(n => n.MaSP == MaSP);

            if (sanpham == null)
            {
                if (LaySoLuong(MaSP) >= 1)
                {
                    sanpham = new GioHang(MaSP);
                    //Thêm vào list

                    lsGioHang.Add(sanpham);
                    return Redirect(url);
                }
                else
                {
                    return Redirect(url);
                }
            }
            else
            {
                if (LaySoLuong(MaSP) > sanpham.SoLuong)
                {
                    sanpham.SoLuong++;
                    return Redirect(url);
                }
                else
                {
                    return Redirect(url);
                }
            }
            
        }

        public int? LaySoLuong(int MaSP)
        {
            int? soluong = (from sl in db.SanPhams where MaSP == sl.MaSP select sl.SoLuong).First();
            return soluong;
        }

        public ActionResult GiamSoLuong(int MaSP, string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lsGioHang = LayGioHang();
            GioHang sanpham = lsGioHang.Find(n => n.MaSP == MaSP);
            if (sanpham.SoLuong >1)
            {
                sanpham.SoLuong -= 1;
                return Redirect(url);
            }
            else
            {
                return Redirect(url);
            }

        }

        public ActionResult XoaSanPham(int MaSP, string url)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lsGioHang = LayGioHang();
            GioHang sanpham = lsGioHang.Find(n => n.MaSP == MaSP);
            lsGioHang.Remove(sanpham);
            return Redirect(url);
        }

        //Xây dựng GioHang
        public ActionResult GioHang()
        {
            List<GioHang> lsGioHang = LayGioHang();
            return View(lsGioHang);
        }

        private int? TongTien()
        {
            int? TongTien = 0;
            List<GioHang> lsGioHang = Session["GioHang"] as List<GioHang>;
            if(lsGioHang != null)
            {
                TongTien = lsGioHang.Sum(n => n.Gia*n.SoLuong);
            }
            return TongTien;
        }

        private int? TongSoLuong()
        {
            int? TongSoLuong = 0;
            List<GioHang> lsGioHang = Session["GioHang"] as List<GioHang>;
            if (lsGioHang != null)
            {
                TongSoLuong = lsGioHang.Sum(n => n.SoLuong);
            }
            return TongSoLuong;
        }

        public ActionResult GioHangPartial()
        {
            if(TongSoLuong()==0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        [HttpPost]
        public ActionResult ThanhToan()
        {
            HoaDon hd = new HoaDon();
            List<GioHang> lsGioHang = LayGioHang();
            hd.NgayLap = DateTime.Now;
            hd.ThanhTien = TongTien();
            //Lấy Mã KH theo Tên đăng nhập
            var id = (from userid in db.KhachHangs where User.Identity.Name == userid.Ten select userid.MaKH).First();
            hd.MaKH = id;
            db.HoaDons.Add(hd);
            foreach (var item in lsGioHang)
            {
                ChiTietHD cthd = new ChiTietHD();
                cthd.MaHD = hd.MaHD;
                cthd.MaSP = item.MaSP;
                cthd.SoLuong = item.SoLuong;
                cthd.Gia = item.Gia;
                db.ChiTietHDs.Add(cthd);
                int? sl = LaySoLuong(item.MaSP) - item.SoLuong;
                var SL = db.SanPhams.First(n => n.MaSP == item.MaSP);
                if(SL!=null)
                {
                    SL.SoLuong = sl;
                    db.Entry(SL).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}