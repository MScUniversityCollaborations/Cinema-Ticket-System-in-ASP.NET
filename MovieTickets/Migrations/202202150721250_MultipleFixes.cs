namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultipleFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NowShowing", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Genres", "Name", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Movies", "Cast", c => c.String(maxLength: 256));
            AlterColumn("dbo.Movies", "DurationMin", c => c.Byte(nullable: false));

            Sql("INSERT INTO Genres (Id, Name) VALUES(1, 'Adventure')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(2, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(3, 'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(4, 'Crime')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(5, 'Mystery')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(6, 'Fantasy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(7, 'Historical')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(8, 'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(9, 'Romance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(10, 'Sci-Fi')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(11, 'Thriller')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(12, 'Western')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(13, 'Other')");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "DurationMin", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Cast", c => c.String(maxLength: 128));
            AlterColumn("dbo.Genres", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movies", "NowShowing");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "GenreId");
        }
    }
}
