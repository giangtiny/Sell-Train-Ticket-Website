namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeginKeyToTimeBetweenStation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TimeBetweenStations", "FirstStationId");
            CreateIndex("dbo.TimeBetweenStations", "SecondStationId");
            AddForeignKey("dbo.TimeBetweenStations", "FirstStationId", "dbo.Stations", "Id");
            AddForeignKey("dbo.TimeBetweenStations", "SecondStationId", "dbo.Stations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeBetweenStations", "SecondStationId", "dbo.Stations");
            DropForeignKey("dbo.TimeBetweenStations", "FirstStationId", "dbo.Stations");
            DropIndex("dbo.TimeBetweenStations", new[] { "SecondStationId" });
            DropIndex("dbo.TimeBetweenStations", new[] { "FirstStationId" });
        }
    }
}
