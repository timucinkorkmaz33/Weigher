namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SoforBilgileri")]
    public partial class SoforBilgileri
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string IsimSoyisim { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefon { get; set; }

        public string Adres { get; set; }

        [StringLength(50)]
        public string TC { get; set; }
    }
}
