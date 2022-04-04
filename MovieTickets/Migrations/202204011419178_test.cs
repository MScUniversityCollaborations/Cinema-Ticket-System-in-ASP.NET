namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScreeningId = c.Byte(nullable: false),
                        UserId = c.Byte(nullable: false),
                        Photo = c.String(),
                        Paid = c.Boolean(nullable: false),
                        PaymentType = c.String(nullable: false, maxLength: 32),
                        Active = c.Boolean(nullable: false),
                        Seat = c.Byte(nullable: false),
                        Screening_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Screenings", t => t.Screening_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Screening_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "Screening_Id", "dbo.Screenings");
            DropIndex("dbo.Reservations", new[] { "User_Id" });
            DropIndex("dbo.Reservations", new[] { "Screening_Id" });
            DropTable("dbo.Reservations");
        }
    }
}
