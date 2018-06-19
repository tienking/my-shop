namespace MyShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }
        [Key]
        public int MaKH { get; set; }
        public int? Quyen { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string Ten { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "{0} không được để trống")]
        [RegularExpression("(\\+84|0)\\d{9,10}",ErrorMessage = "{0} không đúng")]
        public string DienThoai { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} không được để trống")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "{0} không đúng")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string TenDangNhap { get; set; }

        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "{0} không được để trống")]
        public string MatKhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
