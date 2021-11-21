using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SecureApp.Services
{
    public class ValuesService : IValuesService
    {
        private IHttpClientFactory ClientFactory { get; }
        private IConfiguration Configuration { get; }

        public ValuesService(IHttpClientFactory clientFactory,
                             IConfiguration configuration)
        {
            ClientFactory = clientFactory;
            Configuration = configuration;
        }

        public async Task<ValuesGetDto> Get()
        {
            var client = ClientFactory.CreateClient();
            var baseAddress = Configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(baseAddress);

            var response = await client.GetAsync("api/values");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<ValuesGetDto>();

            return result;
        }
    }
}
