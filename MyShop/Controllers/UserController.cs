using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Models;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;

namespace MyShop.Controllers
{
    public class UserController : Controller
    {
        MD5 md5Hash = MD5.Create();
        Model1 db = new Model1();
        DataProtectionSample dps = new DataProtectionSample();
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang kh)
        {
            UserManager um = new UserManager();

            if (ModelState.IsValid)
            {
                if (um.checkUsernameEmail(kh.TenDangNhap, kh.Email))
                {
                    kh.MatKhau = dps.GetMd5Hash(md5Hash, kh.MatKhau);
                    kh.Quyen = 2;
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();

                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError("Register", "Tên Đăng Nhập hoặc Email đã tồn tại");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel user)
        {

            if (ModelState.IsValid)
            {
                string pw = dps.GetMd5Hash(md5Hash, user.Password);
                var check = db.KhachHangs.SingleOrDefault(n => n.TenDangNhap == user.UserName && n.MatKhau == pw);
                if (check != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, true);
                }
                ViewBag.Show = "Sai tên đăng nhập hoặc mật khẩu!!!";
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Redirect to the Home Page (that should be intercepted and redirected to the Login Page first)
            return RedirectToAction("Index", "Home");
        }
    }
}