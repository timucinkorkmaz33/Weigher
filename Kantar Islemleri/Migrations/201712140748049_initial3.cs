namespace Kantar_Islemleri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StokHareketBilgileri", "Cari_Adi", c => c.String());
            AddColumn("dbo.StokHareketBilgileri", "Stok_Adi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StokHareketBilgileri", "Stok_Adi");
            DropColumn("dbo.StokHareketBilgileri", "Cari_Adi");
        }
    }
}
