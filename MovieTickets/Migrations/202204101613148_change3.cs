namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Screenings", "Movie_Id");
        }
        
        public override void Down()
        {
        }
    }
}
