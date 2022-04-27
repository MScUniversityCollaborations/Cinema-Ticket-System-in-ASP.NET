namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSeats7 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Reservations");

            CreateTable(
                "dbo.Reservations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ScreeningId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                    Photo = c.String(),
                    Paid = c.Boolean(nullable: false),
                    PaymentType = c.String(nullable: false, maxLength: 32),
                    Active = c.Boolean(nullable: false),
                    Reserved = c.Boolean(nullable: false),
                    Row = c.Byte(nullable: false),
                    Seat = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Screenings", t => t.ScreeningId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ScreeningId);
        }
        
        public override void Down()
        {
        }
    }
}
