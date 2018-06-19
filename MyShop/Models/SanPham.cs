namespace MyShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHDs = new HashSet<ChiTietHD>();
        }

        [Key]
        public int MaSP { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        public int? Gia { get; set; }

        public int? SoLuong { get; set; }

        public string MoTa { get; set; }

        public int? MaDM { get; set; }

        public string HinhAnh { get; set; }

        public string Mau { get; set; }

        public string GioiTinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHD> ChiTietHDs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
