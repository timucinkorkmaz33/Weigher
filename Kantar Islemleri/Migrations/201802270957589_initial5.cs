namespace Kantar_Islemleri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StokHareketBilgileri", "ToplamFiyat", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StokHareketBilgileri", "ToplamFiyat");
        }
    }
}
