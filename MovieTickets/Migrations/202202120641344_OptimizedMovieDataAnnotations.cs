namespace MovieTickets.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OptimizedMovieDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Movies", "Director", c => c.String(maxLength: 128));
            AlterColumn("dbo.Movies", "Cast", c => c.String(maxLength: 128));
            AlterColumn("dbo.Movies", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Movies", "ImagePoster", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Movies", "ImagePoster", c => c.String());
            AlterColumn("dbo.Movies", "Description", c => c.String());
            AlterColumn("dbo.Movies", "Cast", c => c.String());
            AlterColumn("dbo.Movies", "Director", c => c.String());
            AlterColumn("dbo.Movies", "Title", c => c.String());
        }
    }
}
