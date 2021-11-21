using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
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

            var auth0AuthenticationClient = new AuthenticationApiClient(
                new Uri($"https://{Configuration["Auth:Domain"]}/"));
            var tokenRequest = new ClientCredentialsTokenRequest()
            {
                ClientId = Configuration["Auth:ClientId"],
                ClientSecret = Configuration["Auth:ClientSecret"],
                Audience = Configuration["Services:Values:AuthAudience"]
            };
            var tokenResponse =
                await auth0AuthenticationClient.GetTokenAsync(tokenRequest);

            var baseAddress = Configuration["Services:Values:BaseAddress"];
            client.BaseAddress = new Uri(baseAddress);

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var response = await client.GetAsync("api/values");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsAsync<ValuesGetDto>();

            return result;
        }
    }
}
