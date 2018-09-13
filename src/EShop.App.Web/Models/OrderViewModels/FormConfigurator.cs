using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.OrderViewModels
{
    public class FormConfigurator
    {
        Dictionary<StatusStates, FormConfiguration> Configuration;

        public FormConfigurator()
        {
            Configuration = new Dictionary<StatusStates, FormConfiguration>
            {
                { StatusStates.OnCreating, new FormConfiguration { Action = "Create", Method = "post" } },
                { StatusStates.New, new FormConfiguration { Action = "Modify", Method = "post" } }
            };
        }

        public FormConfiguration GetConfiguration(StatusStates status)
        {
            FormConfiguration configuration;
            if (!Configuration.TryGetValue(status, out configuration))
                throw new InvalidOperationException("Button configuration is not exist for this order");

            return configuration;
        }
    }
}
