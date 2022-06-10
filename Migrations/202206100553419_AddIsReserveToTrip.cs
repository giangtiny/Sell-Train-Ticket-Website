namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsReserveToTrip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "IsReverse", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "IsReverse");
        }
    }
}
