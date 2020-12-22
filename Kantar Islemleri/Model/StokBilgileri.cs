namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StokBilgileri")]
    public partial class StokBilgileri
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Stok_Adi { get; set; }

        [StringLength(50)]
        public string Stok_Kodu { get; set; }

        [StringLength(50)]
        public string Stok_Birim { get; set; }
        [StringLength(50)]
        public string Stok_Birim_Fiyat { get; set; }

        [StringLength(50)]
        public string Stok_KisaAdi { get; set; }

        public DateTime? Stok_Tanim_Tarihi { get; set; }
    }
}
