namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Reservations");
        }
        
        public override void Down()
        {
            DropTable("dbo.Reservations");
        }
    }
}
