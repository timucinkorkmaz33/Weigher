namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CariBilgileri")]
    public partial class CariBilgileri
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Cari_Kodu { get; set; }

        [Required]
        [StringLength(250)]
        public string Cari_Adi { get; set; }

        public DateTime? Cari_Kayit_Tarihi { get; set; }

        [StringLength(50)]
        public string Cari_Vergi_Dairesi { get; set; }

        [StringLength(50)]
        public string Cari_Vergi_No { get; set; }

        [StringLength(50)]
        public string Cari_Telefon { get; set; }

        public string Cari_Adres1 { get; set; }

        public string Cari_Adres2 { get; set; }

        [StringLength(50)]
        public string Cari_Yetkili { get; set; }

        [StringLength(50)]
        public string Cari_Yetkili_Telefon { get; set; }

        [StringLength(50)]
        public string Cari_Yetkili_EMail { get; set; }
    }
}
