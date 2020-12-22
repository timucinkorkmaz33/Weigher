namespace Kantar_Islemleri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StokHareketBilgileri", "Cari_Kodu", c => c.String(maxLength: 50));
            AddColumn("dbo.StokHareketBilgileri", "Fiyat", c => c.String(maxLength: 50));
            AddColumn("dbo.StokHareketBilgileri", "Aciklama4", c => c.String());
            AddColumn("dbo.StokHareketBilgileri", "Aciklama5", c => c.String());
            AddColumn("dbo.StokHareketBilgileri", "Aciklama6", c => c.String());
            AddColumn("dbo.StokHareketBilgileri", "Aciklama7", c => c.String());
            AddColumn("dbo.StokHareketBilgileri", "Aciklama8", c => c.String());
            DropColumn("dbo.StokHareketBilgileri", "Aciklama");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StokHareketBilgileri", "Aciklama", c => c.String());
            DropColumn("dbo.StokHareketBilgileri", "Aciklama8");
            DropColumn("dbo.StokHareketBilgileri", "Aciklama7");
            DropColumn("dbo.StokHareketBilgileri", "Aciklama6");
            DropColumn("dbo.StokHareketBilgileri", "Aciklama5");
            DropColumn("dbo.StokHareketBilgileri", "Aciklama4");
            DropColumn("dbo.StokHareketBilgileri", "Fiyat");
            DropColumn("dbo.StokHareketBilgileri", "Cari_Kodu");
        }
    }
}
