using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.OrderViewModels
{
    public class ButtonConfigurator
    {      
        Dictionary<Commands, ButtonConfiguration> Configuration;

        public ButtonConfigurator()
        {
            Configuration = new Dictionary<Commands, ButtonConfiguration>
            {
                {Commands.Confirm, new ButtonConfiguration{ ActionName = "Confirm", HttpType = "patch", Url = "Orders/api/" } },
                {Commands.Pay, new ButtonConfiguration{ ActionName = "Pay", HttpType = "patch", Url = "Orders/api/" } },
                {Commands.Pack, new ButtonConfiguration{ ActionName = "Pack", HttpType = "patch", Url = "Orders/api/" } },
                {Commands.Deliver, new ButtonConfiguration{ ActionName = "Deliver", HttpType = "patch", Url = "Orders/api/" } },
                {Commands.Complete, new ButtonConfiguration{ ActionName = "Complete", HttpType = "patch", Url = "Orders/api/" } },
                {Commands.Nothing, null }
            };
        }

        public ButtonConfiguration GetConfiguration(Commands command)
        {
            ButtonConfiguration configuration;
            if (!Configuration.TryGetValue(command, out configuration))
                throw new InvalidOperationException("Button configuration is not exist for this order");

            return configuration;
        }
    }
}
