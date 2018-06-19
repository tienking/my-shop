using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class GioHang
    {
        Model1 db = new Model1();
        public int MaSP { get; set; }

        public string Ten { get; set; }

        public int? Gia { get; set; }

        public string HinhAnh { get; set; }

        public int? SoLuong { get; set; }

        public int? ThanhTien
        {
            get
            {
                return SoLuong * Gia;
            }
        }

        public GioHang(int MaSP)
        {
            this.MaSP = MaSP;
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            Ten = sp.Ten;
            HinhAnh = sp.HinhAnh;
            Gia = sp.Gia;
            SoLuong = 1;
        }
    }
}