namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class audFix2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoriums",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 32),
                    TotalSeats = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Screenings", "AuditoriumId", c => c.Int());
            CreateIndex("dbo.Screenings", "AuditoriumId");
            AddForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
