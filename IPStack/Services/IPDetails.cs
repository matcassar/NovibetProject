using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IPStackDLL.Models
{
    public interface IPDetails
    {
        string ip { get; set; }

        string continent_code { get; set; }
        string continent_name { get; set; }
        string country_code { get; set; }
        string country_name { get; set; }
        int region_code { get; set; }
        string region_name { get; set; }
        string zip { get; set; }
        string city { get; set; }

        double latitude { get; set; }

        double longitude { get; set; }
       
        Guid ipGuid { get; set; }
    }
}
