namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAudIdToInt1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Screenings", "Auditorium_Id", "dbo.Auditoriums");
            DropIndex("dbo.Screenings", new[] { "Auditorium_Id" });
            DropColumn("dbo.Screenings", "AuditoriumId");
            RenameColumn(table: "dbo.Screenings", name: "Auditorium_Id", newName: "AuditoriumId");
            AlterColumn("dbo.Screenings", "AuditoriumId", c => c.Int(nullable: false));
            AlterColumn("dbo.Screenings", "AuditoriumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Screenings", "AuditoriumId");
            AddForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Screenings", "AuditoriumId", "dbo.Auditoriums");
            DropIndex("dbo.Screenings", new[] { "AuditoriumId" });
            AlterColumn("dbo.Screenings", "AuditoriumId", c => c.Int());
            AlterColumn("dbo.Screenings", "AuditoriumId", c => c.Byte(nullable: false));
            RenameColumn(table: "dbo.Screenings", name: "AuditoriumId", newName: "Auditorium_Id");
            AddColumn("dbo.Screenings", "AuditoriumId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Screenings", "Auditorium_Id");
            AddForeignKey("dbo.Screenings", "Auditorium_Id", "dbo.Auditoriums", "Id");
        }
    }
}
