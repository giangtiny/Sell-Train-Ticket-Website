namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStationClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "IsFirst", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stations", "IsFinal", c => c.Boolean(nullable: false));
            DropColumn("dbo.Routes", "DepartureStationId");
            DropColumn("dbo.Routes", "DestinationStationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "DestinationStationId", c => c.Int(nullable: false));
            AddColumn("dbo.Routes", "DepartureStationId", c => c.Int(nullable: false));
            DropColumn("dbo.Stations", "IsFinal");
            DropColumn("dbo.Stations", "IsFirst");
        }
    }
}
