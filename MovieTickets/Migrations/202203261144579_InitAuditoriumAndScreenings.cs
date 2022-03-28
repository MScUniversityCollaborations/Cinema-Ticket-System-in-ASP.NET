namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitAuditoriumAndScreenings : DbMigration
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
                    Id = c.Int(nullable: false),
                    MovieId = c.Int(nullable: false),
                    AuditoriumId = c.Byte(nullable: false),
                    ScreeningStart = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .PrimaryKey(t => t.AuditoriumId)
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Auditoriums", t => t.AuditoriumId, cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
