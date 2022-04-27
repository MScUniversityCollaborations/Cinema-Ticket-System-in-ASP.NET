namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSeats2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Auditoriums", "SeatsPerRow");
            //AddColumn("dbo.Auditoriums", "Rows", c => c.Byte(nullable: false));
            AddColumn("dbo.Auditoriums", "SeatsPerRow", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}
