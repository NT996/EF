namespace CFProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Autor = c.String(),
                        Price = c.Single(),
                        PublisherID = c.Int(nullable: false),
                        GenreID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genres", t => t.GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherID, cascadeDelete: true)
                .Index(t => t.PublisherID)
                .Index(t => t.GenreID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "PublisherID", "dbo.Publishers");
            DropForeignKey("dbo.Books", "GenreID", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "GenreID" });
            DropIndex("dbo.Books", new[] { "PublisherID" });
            DropTable("dbo.Publishers");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
        }
    }
}
