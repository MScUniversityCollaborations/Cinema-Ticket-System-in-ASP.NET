namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSeats3 : DbMigration
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
                    Rows = c.Byte(nullable: false),
                    SeatsPerRow = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Screenings", "AuditoriumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Screenings", "AuditoriumId");
            AddForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
