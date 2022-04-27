namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSeats : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Auditoriums", "TotalSeats");
            AddColumn("dbo.Auditoriums", "Rows", c => c.Byte(nullable: false));
            AddColumn("dbo.Auditoriums", "SeatsPerRow", c => c.Int(nullable: false));
        }

        public override void Down()
        {
        }
    }
}
