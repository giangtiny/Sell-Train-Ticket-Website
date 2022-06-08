namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTripStatisticModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TripStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TripId = c.Int(nullable: false),
                        Revenue = c.Long(nullable: false),
                        TicketInStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TripStatistics", "TripId", "dbo.Trips");
            DropIndex("dbo.TripStatistics", new[] { "TripId" });
            DropTable("dbo.TripStatistics");
        }
    }
}
