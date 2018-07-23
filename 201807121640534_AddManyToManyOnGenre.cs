namespace CFProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyOnGenre : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Book_ID", "dbo.Books");
            DropIndex("dbo.Genres", new[] { "Book_ID" });
            CreateTable(
                "dbo.GenreBooks",
                c => new
                    {
                        Genre_ID = c.Int(nullable: false),
                        Book_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_ID, t.Book_ID })
                .ForeignKey("dbo.Genres", t => t.Genre_ID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_ID, cascadeDelete: true)
                .Index(t => t.Genre_ID)
                .Index(t => t.Book_ID);
            
            DropColumn("dbo.Genres", "Book_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "Book_ID", c => c.Int());
            DropForeignKey("dbo.GenreBooks", "Book_ID", "dbo.Books");
            DropForeignKey("dbo.GenreBooks", "Genre_ID", "dbo.Genres");
            DropIndex("dbo.GenreBooks", new[] { "Book_ID" });
            DropIndex("dbo.GenreBooks", new[] { "Genre_ID" });
            DropTable("dbo.GenreBooks");
            CreateIndex("dbo.Genres", "Book_ID");
            AddForeignKey("dbo.Genres", "Book_ID", "dbo.Books", "ID");
        }
    }
}
