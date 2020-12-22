namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StokHareketBilgileri")]
    public partial class StokHareketBilgileri
    {
        public int Id { get; set; }

        public DateTime Islem_Tarihi { get; set; }

        [Required]
        [StringLength(50)]
        public string Arac_Plaka { get; set; }

        public string Sofor_Adi { get; set; }

        [StringLength(50)]
        public string Cari_Kodu { get; set; }

        [StringLength(50)]
        public string Stok_Kodu { get; set; }

        public string Cari_Adi { get; set; }
        
        public string Stok_Adi { get; set; }

        [StringLength(50)]
        public string Stok_Miktar { get; set; }
        [StringLength(50)]
        public string Fiyat { get; set; }

        public string IrsaliyeNo { get; set; }

        [StringLength(50)]
        public string Evrak_Seri { get; set; }

        public int? Evrak_No { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Aciklama4 { get; set; }
        public string Aciklama5 { get; set; }
        public string Aciklama6 { get; set; }
        public string Aciklama7 { get; set; }
        public string Aciklama8 { get; set; }
        public double ToplamFiyat { get; set; }

    }
}
