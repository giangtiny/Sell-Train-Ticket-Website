namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRouteClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "DepartureStationId", c => c.Int(nullable: true));
            AddColumn("dbo.Routes", "DestinationStationId", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "DestinationStationId");
            DropColumn("dbo.Routes", "DepartureStationId");
        }
    }
}
