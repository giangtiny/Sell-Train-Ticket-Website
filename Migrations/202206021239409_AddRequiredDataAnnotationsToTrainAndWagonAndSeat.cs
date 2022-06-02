namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredDataAnnotationsToTrainAndWagonAndSeat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Seats", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Wagons", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Trains", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trains", "Name", c => c.String());
            AlterColumn("dbo.Wagons", "Name", c => c.String());
            AlterColumn("dbo.Seats", "Name", c => c.String());
        }
    }
}
