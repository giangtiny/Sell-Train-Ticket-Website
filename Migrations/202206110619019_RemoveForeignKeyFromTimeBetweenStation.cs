namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForeignKeyFromTimeBetweenStation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeBetweenStations", "FirstStationId", "dbo.Stations");
            DropForeignKey("dbo.TimeBetweenStations", "SecondStationId", "dbo.Stations");
            DropIndex("dbo.TimeBetweenStations", new[] { "FirstStationId" });
            DropIndex("dbo.TimeBetweenStations", new[] { "SecondStationId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TimeBetweenStations", "SecondStationId");
            CreateIndex("dbo.TimeBetweenStations", "FirstStationId");
            AddForeignKey("dbo.TimeBetweenStations", "SecondStationId", "dbo.Stations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TimeBetweenStations", "FirstStationId", "dbo.Stations", "Id", cascadeDelete: true);
        }
    }
}
