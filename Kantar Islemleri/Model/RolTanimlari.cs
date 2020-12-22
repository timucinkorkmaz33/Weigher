namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RolTanimlari")]
    public partial class RolTanimlari
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RolAdi { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Discriminator { get; set; }
    }
}
