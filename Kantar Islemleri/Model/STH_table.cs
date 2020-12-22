namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STH_table
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Islem_Tarihi { get; set; }

        [StringLength(50)]
        public string Arac_Plaka { get; set; }

        public string Sofor_Adi { get; set; }

        [StringLength(50)]
        public string Stok_Kodu { get; set; }

        [StringLength(50)]
        public string Stok_Miktar { get; set; }

        [StringLength(50)]
        public string Evrak_Seri { get; set; }

        public int? Evrak_No { get; set; }

        public string Aciklama { get; set; }

        public string Aciklama1 { get; set; }

        public string Aciklama2 { get; set; }

        public string Aciklama3 { get; set; }
    }
}
