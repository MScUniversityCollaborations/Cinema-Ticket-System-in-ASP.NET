namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Screenings", new[] { "Movie_Id" });
            DropIndex("dbo.Screenings", "IX_MovieId", "dbo.Movies");
            DropIndex("dbo.Screenings", new[] { "Movie_Id" });
            DropColumn("dbo.Screenings", "MovieId");
            RenameColumn(table: "dbo.Screenings", name: "Movie_Id", newName: "MovieId");
            AlterColumn("dbo.Screenings", "MovieId", c => c.Int(nullable: false));
            AlterColumn("dbo.Screenings", "MovieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Screenings", "MovieId");
            AddForeignKey("dbo.Screenings", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Screenings", "MovieId", "dbo.Movies");
            DropIndex("dbo.Screenings", new[] { "MovieId" });
            AlterColumn("dbo.Screenings", "MovieId", c => c.Int());
            AlterColumn("dbo.Screenings", "MovieId", c => c.Byte(nullable: false));
            RenameColumn(table: "dbo.Screenings", name: "MovieId", newName: "Movie_Id");
            AddColumn("dbo.Screenings", "MovieId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Screenings", "Movie_Id");
            AddForeignKey("dbo.Screenings", "Movie_Id", "dbo.Movies", "Id");
        }
    }
}
