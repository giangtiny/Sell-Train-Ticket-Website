namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartureTimeAndArrivalTimeToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "DepartureTime", c => c.String(nullable: false));
            AddColumn("dbo.Trips", "ArrivalTime", c => c.String(nullable: false));
            CreateIndex("dbo.Seats", "WagonId");
            CreateIndex("dbo.Seats", "SeatTypeId");
            CreateIndex("dbo.Wagons", "TrainId");
            AddForeignKey("dbo.Seats", "SeatTypeId", "dbo.SeatTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Wagons", "TrainId", "dbo.Trains", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Seats", "WagonId", "dbo.Wagons", "Id", cascadeDelete: true);
            DropColumn("dbo.Trips", "MovingTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "MovingTime", c => c.Int(nullable: false));
            DropForeignKey("dbo.Seats", "WagonId", "dbo.Wagons");
            DropForeignKey("dbo.Wagons", "TrainId", "dbo.Trains");
            DropForeignKey("dbo.Seats", "SeatTypeId", "dbo.SeatTypes");
            DropIndex("dbo.Wagons", new[] { "TrainId" });
            DropIndex("dbo.Seats", new[] { "SeatTypeId" });
            DropIndex("dbo.Seats", new[] { "WagonId" });
            DropColumn("dbo.Trips", "ArrivalTime");
            DropColumn("dbo.Trips", "DepartureTime");
        }
    }
}
