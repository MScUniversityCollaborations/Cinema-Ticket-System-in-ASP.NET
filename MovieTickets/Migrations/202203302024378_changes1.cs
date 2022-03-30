namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                {
                    Id = c.Int(nullable: false),
                    ScreeningId = c.Int(nullable: false),
                    UserId = c.String(nullable: false, maxLength: 128),
                    Reserved = c.Boolean(nullable: false),
                    Photo = c.String(nullable: true),
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
        {}
    }
}
