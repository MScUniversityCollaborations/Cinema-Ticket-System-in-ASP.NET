namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSeats5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums");
            DropIndex("dbo.Screenings", new[] { "AuditoriumId" });
            DropColumn("dbo.Screenings", "AuditoriumId");

            DropTable("dbo.Auditoriums");

            CreateTable(
                "dbo.Auditoriums",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 32),
                    TotalSeats = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Screenings", "AuditoriumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Screenings", "AuditoriumId");
            AddForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums", "Id");

            AddColumn("dbo.Auditoriums", "Rows", c => c.Byte(nullable: false));
            AddColumn("dbo.Auditoriums", "SeatsPerRows", c => c.Byte(nullable: false));
            // AddColumn("dbo.Reservations", "Row", c => c.Byte(nullable: false));
            DropColumn("dbo.Auditoriums", "TotalSeats");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auditoriums", "TotalSeats", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "Row");
            DropColumn("dbo.Auditoriums", "SeatsPerRows");
            DropColumn("dbo.Auditoriums", "Rows");
        }
    }
}
