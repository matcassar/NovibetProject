using IPStackDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPStackDLL
{
   public  interface IIPInfoProvider
    {
        IPDetails GetDetails(string ip);

        Task<IPDetails> GetDetailsAsync(string ip);
    }

}
