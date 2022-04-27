namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class audFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums");
            DropIndex("dbo.Screenings", new[] { "AuditoriumId" });
            DropColumn("dbo.Screenings", "AuditoriumId");

            DropTable("dbo.Auditoriums");
        }
        
        public override void Down()
        {
        }
    }
}
