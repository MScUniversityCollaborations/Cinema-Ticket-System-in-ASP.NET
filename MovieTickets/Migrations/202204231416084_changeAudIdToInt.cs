namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAudIdToInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums");
            DropIndex("dbo.Screenings", new[] { "AuditoriumId" });
            DropPrimaryKey("dbo.Auditoriums");
            AddColumn("dbo.Screenings", "Auditorium_Id", c => c.Int());
            AlterColumn("dbo.Auditoriums", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Auditoriums", "Id");
            CreateIndex("dbo.Screenings", "Auditorium_Id");
            AddForeignKey("dbo.Screenings", "Auditorium_Id", "dbo.Auditoriums", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Screenings", "Auditorium_Id", "dbo.Auditoriums");
            DropIndex("dbo.Screenings", new[] { "Auditorium_Id" });
            DropPrimaryKey("dbo.Auditoriums");
            AlterColumn("dbo.Auditoriums", "Id", c => c.Byte(nullable: false));
            DropColumn("dbo.Screenings", "Auditorium_Id");
            AddPrimaryKey("dbo.Auditoriums", "Id");
            CreateIndex("dbo.Screenings", "AuditoriumId");
            AddForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums", "Id", cascadeDelete: true);
        }
    }
}
