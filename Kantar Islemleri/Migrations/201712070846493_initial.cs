namespace Kantar_Islemleri.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YaziciBilgileri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Yazici1 = c.String(),
                        Yazici2 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.YaziciBilgileri");
        }
    }
}
