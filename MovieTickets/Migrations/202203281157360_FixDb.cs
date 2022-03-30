namespace MovieTickets.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoriums",
                c => new
                {
                    Id = c.Byte(nullable: false),
                    Name = c.String(nullable: false, maxLength: 32),
                    TotalSeats = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Screenings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AuditoriumId = c.Byte(nullable: false),
                    MovieId = c.Byte(nullable: false),
                    ScreeningStart = c.DateTime(nullable: false),
                    Movie_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditoriums", t => t.AuditoriumId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .Index(t => t.AuditoriumId)
                .Index(t => t.Movie_Id);

            CreateTable(
                "dbo.Reservations",
                c => new
                {
                    Id = c.Int(nullable: false),
                    ScreeningId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                    Reserved = c.Boolean(nullable: false),
                    Photo = c.String(nullable: false),
                    Paid = c.Boolean(nullable: false),
                    PaymentType = c.String(nullable: false),
                    Active = c.Boolean(nullable: false),
                    Seat = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ScreeningId)
                .Index(t => t.UserId);

            AddForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "ScreeningId", "dbo.Screenings", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Screenings", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums");
            DropIndex("dbo.Screenings", new[] { "Movie_Id" });
            DropIndex("dbo.Screenings", new[] { "AuditoriumId" });
            DropTable("dbo.Screenings");
            DropTable("dbo.Auditoriums");
        }
    }
}
