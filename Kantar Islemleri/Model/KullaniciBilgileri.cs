namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KullaniciBilgileri")]
    public partial class KullaniciBilgileri
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AdiSoyadi { get; set; }

        [Required]
        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        [Required]
        [StringLength(50)]
        public string Sifre { get; set; }

        [StringLength(500)]
        public string Adres { get; set; }

        [StringLength(50)]
        public string TelefonNo { get; set; }

        [StringLength(50)]
        public string EMail { get; set; }

        public string Roles { get; set; }
    }
}
