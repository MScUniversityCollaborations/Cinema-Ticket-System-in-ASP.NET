namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenres : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Thriller')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Family')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Romance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Comedy')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Adventure')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Horror')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (8, 'Sci-Fi')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (9, 'Western')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (10, 'Documentary')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (11, 'Drama')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (12, 'Crime')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (13, 'Historical')");

        }

        public override void Down()
        {
        }
    }
}
