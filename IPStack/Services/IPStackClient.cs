using IPStackDLL.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPStackDLL.Models
{
    public class IpStackClient : IIPInfoProvider
    {
        private const string NotSuccess = "false";


        public IPDetails GetDetails(string ip)
        {
            var ipDetails = GetDetailsAsync(ip).GetAwaiter().GetResult();

            return ipDetails;
        }

        public async Task<IPDetails> GetDetailsAsync(string ip)
        {
            var baseURL = "http://api.ipstack.com/";
            var accessKey = "b3bc556acfd11183634ead98ae2abac4";
            var url = baseURL + ip + "? access_key =" + accessKey;
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new IPServiceNotAvailableException($"IpStackInfoProvider exception occurred, status code: {response.StatusCode}");
            }

            var jsonResult = response.Content;

            var ipDetails = ConvertToIpDetails(jsonResult);

            return ipDetails;
        }

        private IPDetails ConvertToIpDetails(string response)
        {
            var settings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                NullValueHandling = NullValueHandling.Ignore,
            };

            var ipStackResponse = JsonConvert.DeserializeObject<IpResponse>(response, settings);

            if (ipStackResponse.Success == NotSuccess)
            {
                throw new IPServiceNotAvailableException($"Some invalid properties where passed to provider.");
            }

            return ipStackResponse;
        }
    }
}
