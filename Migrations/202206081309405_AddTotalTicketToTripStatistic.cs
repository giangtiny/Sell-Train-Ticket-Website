namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalTicketToTripStatistic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TripStatistics", "TotalTicket", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TripStatistics", "TotalTicket");
        }
    }
}
