namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTripClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "DepartureTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Trips", "ArrivalTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "ArrivalTime", c => c.String(nullable: false));
            AlterColumn("dbo.Trips", "DepartureTime", c => c.String(nullable: false));
        }
    }
}
