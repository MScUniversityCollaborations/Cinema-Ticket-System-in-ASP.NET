namespace MovieTickets.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFirstAndLastNameToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 256));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
