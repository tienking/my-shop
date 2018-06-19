using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyShop.Models;

namespace MyShop.Models
{
    public class UserManager
    {
        Model1 db = new Model1();
        public bool checkUsernameEmail(String un, String em)
        {
            if (db.KhachHangs.Any(r => r.TenDangNhap == un) || db.KhachHangs.Any(r => r.Email == em))
            {
                return false;
            }
            else return true;
        }

        public bool IsValidEmailAddress(string email)
        {
            try
            {
                var emailChecked = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}