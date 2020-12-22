namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AracBilgileri")]
    public partial class AracBilgileri
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string AracMarka { get; set; }

        [Required]
        [StringLength(50)]
        public string Plaka { get; set; }

       
        [StringLength(50)]
        public string DorsePlaka { get; set; }

        [StringLength(50)]
        public string AracModeli { get; set; }

        public int Tartım1 { get; set; }
    }
}
