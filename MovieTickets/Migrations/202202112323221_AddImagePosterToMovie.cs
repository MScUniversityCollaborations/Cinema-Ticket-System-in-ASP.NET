namespace MovieTickets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePosterToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImagePoster", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "ImagePoster");
        }
    }
}
