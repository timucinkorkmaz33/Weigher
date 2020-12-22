namespace Kantar_Islemleri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StokHareketBilgileri", "IrsaliyeNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StokHareketBilgileri", "IrsaliyeNo");
        }
    }
}
