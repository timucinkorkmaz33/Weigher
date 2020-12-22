namespace Kantar_Islemleri
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Model;

    public partial class DoraKantar : DbContext
    {
        public DoraKantar()
            : base("name=DoraKantar")
        {
        }

       
        public virtual DbSet<AracBilgileri> AracBilgileri { get; set; }
        public virtual DbSet<CariBilgileri> CariBilgileri { get; set; }
        public virtual DbSet<KullaniciBilgileri> KullaniciBilgileri { get; set; }
        public virtual DbSet<RolKullanici> RolKullanici { get; set; }
        public virtual DbSet<RolTanimlari> RolTanimlari { get; set; }
        public virtual DbSet<SoforBilgileri> SoforBilgileri { get; set; }
        public virtual DbSet<StokBilgileri> StokBilgileri { get; set; }
        public virtual DbSet<StokHareketBilgileri> StokHareketBilgileri { get; set; }
        public virtual DbSet<STH_table> STH_table { get; set; }
        public virtual DbSet<YaziciBilgileri> YaziciBilgileri { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
