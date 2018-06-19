using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class LoginModel
    {
        public int? Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}