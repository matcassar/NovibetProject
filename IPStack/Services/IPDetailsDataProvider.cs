using IPStackDLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPStackDLL.Services
{
    public interface IPDetailsDataProvider
    {
        IPDetails GetIPDetails(string ip);

        void InsertIPDetails(IPDetails details);

        void SaveIPDetails(IPDetails details);

        void UpdateIPDetails(IPDetails details);

    }
}
