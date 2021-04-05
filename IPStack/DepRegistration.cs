using IPStackDLL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPStackDLL
{
    public class DepRegistration
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<IIPInfoProvider>();
        }
    }
}
