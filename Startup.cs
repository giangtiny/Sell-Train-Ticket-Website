using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sell_Train_Ticket.Startup))]
namespace Sell_Train_Ticket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
