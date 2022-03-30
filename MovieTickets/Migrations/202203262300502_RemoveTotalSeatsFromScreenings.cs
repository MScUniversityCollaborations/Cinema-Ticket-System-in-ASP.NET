namespace MovieTickets.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveTotalSeatsFromScreenings : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Screenings", "TotalSeats");
        }

        public override void Down()
        {
        }
    }
}
