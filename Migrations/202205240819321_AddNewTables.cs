namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WagonId = c.Int(nullable: false),
                        SeatTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SeatTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ParkingTime = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        TripId = c.Int(nullable: false),
                        DepartureStationId = c.Int(nullable: false),
                        DestinationStationId = c.Int(nullable: false),
                        SeatId = c.Int(nullable: false),
                        IsKid = c.Boolean(nullable: false),
                        State = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.DepartureStationId, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.DestinationStationId, cascadeDelete: true)
                .ForeignKey("dbo.Seats", t => t.SeatId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.TripId)
                .Index(t => t.DepartureStationId)
                .Index(t => t.DestinationStationId)
                .Index(t => t.SeatId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RouteId = c.Int(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        MovingTime = c.Int(nullable: false),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Trains", t => t.TrainId, cascadeDelete: true)
                .Index(t => t.RouteId)
                .Index(t => t.TrainId);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeBetweenStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstStationId = c.Int(nullable: false),
                        SecondStationId = c.Int(nullable: false),
                        MovingTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wagons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Trips", "TrainId", "dbo.Trains");
            DropForeignKey("dbo.Trips", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Tickets", "SeatId", "dbo.Seats");
            DropForeignKey("dbo.Tickets", "DestinationStationId", "dbo.Stations");
            DropForeignKey("dbo.Tickets", "DepartureStationId", "dbo.Stations");
            DropForeignKey("dbo.Tickets", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stations", "RouteId", "dbo.Routes");
            DropIndex("dbo.Trips", new[] { "TrainId" });
            DropIndex("dbo.Trips", new[] { "RouteId" });
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            DropIndex("dbo.Tickets", new[] { "DestinationStationId" });
            DropIndex("dbo.Tickets", new[] { "DepartureStationId" });
            DropIndex("dbo.Tickets", new[] { "TripId" });
            DropIndex("dbo.Tickets", new[] { "CustomerId" });
            DropIndex("dbo.Stations", new[] { "RouteId" });
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String());
            DropTable("dbo.Wagons");
            DropTable("dbo.TimeBetweenStations");
            DropTable("dbo.Trains");
            DropTable("dbo.Trips");
            DropTable("dbo.Tickets");
            DropTable("dbo.Stations");
            DropTable("dbo.SeatTypes");
            DropTable("dbo.Seats");
            DropTable("dbo.Routes");
        }
    }
}
