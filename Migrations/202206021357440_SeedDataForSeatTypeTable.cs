namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDataForSeatTypeTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO SeatTypes (Name, Price) VALUES ('Hard Seat', 60000)");
            Sql("INSERT INTO SeatTypes (Name, Price) VALUES ('Soft Seat', 90000)");
            Sql("INSERT INTO SeatTypes (Name, Price) VALUES ('Bed', 120000)");
        }
        
        public override void Down()
        {
        }
    }
}
