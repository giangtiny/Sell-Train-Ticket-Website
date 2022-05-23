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
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeatType_Id = c.Int(),
                        Wagon_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeatTypes", t => t.SeatType_Id)
                .ForeignKey("dbo.Wagons", t => t.Wagon_Id)
                .Index(t => t.SeatType_Id)
                .Index(t => t.Wagon_Id);
            
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
                "dbo.Wagons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Train_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trains", t => t.Train_Id)
                .Index(t => t.Train_Id);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.Trip_Id)
                .Index(t => t.Trip_Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartureDate = c.DateTime(nullable: false),
                        MovingTime = c.Int(nullable: false),
                        Route_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.Route_Id)
                .Index(t => t.Route_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParkingTime = c.Int(nullable: false),
                        Route_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.Route_Id)
                .Index(t => t.Route_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsKid = c.Boolean(nullable: false),
                        State = c.Boolean(nullable: false),
                        Customer_Id = c.String(maxLength: 128),
                        DepartureStation_Id = c.Int(),
                        DestinationStation_Id = c.Int(),
                        Seat_Id = c.Int(),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .ForeignKey("dbo.Stations", t => t.DepartureStation_Id)
                .ForeignKey("dbo.Stations", t => t.DestinationStation_Id)
                .ForeignKey("dbo.Seats", t => t.Seat_Id)
                .ForeignKey("dbo.Trips", t => t.Trip_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.DepartureStation_Id)
                .Index(t => t.DestinationStation_Id)
                .Index(t => t.Seat_Id)
                .Index(t => t.Trip_Id);
            
            CreateTable(
                "dbo.TimeBetweenStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovingTime = c.Int(nullable: false),
                        FirstStation_Id = c.Int(),
                        SecondStation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.FirstStation_Id)
                .ForeignKey("dbo.Stations", t => t.SecondStation_Id)
                .Index(t => t.FirstStation_Id)
                .Index(t => t.SecondStation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeBetweenStations", "SecondStation_Id", "dbo.Stations");
            DropForeignKey("dbo.TimeBetweenStations", "FirstStation_Id", "dbo.Stations");
            DropForeignKey("dbo.Tickets", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Tickets", "Seat_Id", "dbo.Seats");
            DropForeignKey("dbo.Tickets", "DestinationStation_Id", "dbo.Stations");
            DropForeignKey("dbo.Tickets", "DepartureStation_Id", "dbo.Stations");
            DropForeignKey("dbo.Tickets", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Stations", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.Seats", "Wagon_Id", "dbo.Wagons");
            DropForeignKey("dbo.Wagons", "Train_Id", "dbo.Trains");
            DropForeignKey("dbo.Trains", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Trips", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.Seats", "SeatType_Id", "dbo.SeatTypes");
            DropIndex("dbo.TimeBetweenStations", new[] { "SecondStation_Id" });
            DropIndex("dbo.TimeBetweenStations", new[] { "FirstStation_Id" });
            DropIndex("dbo.Tickets", new[] { "Trip_Id" });
            DropIndex("dbo.Tickets", new[] { "Seat_Id" });
            DropIndex("dbo.Tickets", new[] { "DestinationStation_Id" });
            DropIndex("dbo.Tickets", new[] { "DepartureStation_Id" });
            DropIndex("dbo.Tickets", new[] { "Customer_Id" });
            DropIndex("dbo.Stations", new[] { "Route_Id" });
            DropIndex("dbo.Trips", new[] { "Route_Id" });
            DropIndex("dbo.Trains", new[] { "Trip_Id" });
            DropIndex("dbo.Wagons", new[] { "Train_Id" });
            DropIndex("dbo.Seats", new[] { "Wagon_Id" });
            DropIndex("dbo.Seats", new[] { "SeatType_Id" });
            DropTable("dbo.TimeBetweenStations");
            DropTable("dbo.Tickets");
            DropTable("dbo.Stations");
            DropTable("dbo.Trips");
            DropTable("dbo.Trains");
            DropTable("dbo.Wagons");
            DropTable("dbo.SeatTypes");
            DropTable("dbo.Seats");
            DropTable("dbo.Routes");
        }
    }
}
