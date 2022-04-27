namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSeats4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Row", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}
