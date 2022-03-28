namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeatsAndReservedSeatsTables : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
                "dbo.Seats",
                c => new
                {
                    Id = c.Int(nullable: false),
                    AuditoriumId = c.Byte(nullable: false),
                    Rows = c.Int(nullable: false),
                    TotalSeats = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AuditoriumId);
                
            AddForeignKey("dbo.Seats", "AuditoriumId", "dbo.Auditoriums", "Id", cascadeDelete: true);*/

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

           /* CreateTable(
                "dbo.ReservedSeats",
                c => new
                {
                    Id = c.Int(nullable: false),
                    ReservationId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .Index(t => t.ReservationId);*/
        }
        
        public override void Down()
        {
        }
    }
}
