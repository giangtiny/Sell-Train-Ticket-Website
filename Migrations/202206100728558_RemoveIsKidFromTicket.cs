namespace Sell_Train_Ticket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsKidFromTicket : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "IsKid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "IsKid", c => c.Boolean(nullable: false));
        }
    }
}
