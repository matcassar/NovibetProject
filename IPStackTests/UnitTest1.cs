using System;
using System.Threading.Tasks;
using IPStackDLL.Exceptions;
using IPStackDLL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPStackTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetIPAddresses()
        {

            var ipStackInfoProvider = new IpStackClient();
            var ip = "141.8.56.32";

            Func<Task> action = async () => { await ipStackInfoProvider.GetDetailsAsync(ip); };
     
        }
    }
}
