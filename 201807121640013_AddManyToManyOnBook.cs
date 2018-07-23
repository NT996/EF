namespace CFProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyOnBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "GenreID", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "GenreID" });
            AddColumn("dbo.Genres", "Book_ID", c => c.Int());
            CreateIndex("dbo.Genres", "Book_ID");
            AddForeignKey("dbo.Genres", "Book_ID", "dbo.Books", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genres", "Book_ID", "dbo.Books");
            DropIndex("dbo.Genres", new[] { "Book_ID" });
            DropColumn("dbo.Genres", "Book_ID");
            CreateIndex("dbo.Books", "GenreID");
            AddForeignKey("dbo.Books", "GenreID", "dbo.Genres", "ID", cascadeDelete: true);
        }
    }
}
