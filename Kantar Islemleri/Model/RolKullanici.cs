namespace Kantar_Islemleri
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RolKullanici")]
    public partial class RolKullanici
    {
        public int Id { get; set; }

        public int RolId { get; set; }

        public int UserId { get; set; }
    }
}
