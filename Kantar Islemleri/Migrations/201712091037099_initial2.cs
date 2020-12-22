namespace Kantar_Islemleri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StokBilgileri", "Stok_Birim_Fiyat", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StokBilgileri", "Stok_Birim_Fiyat");
        }
    }
}
