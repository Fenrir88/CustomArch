using BlazorUI.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace BlazorUI.BlazorWebService
{
    public class BlazorWebServiceImplementation : IBlazorWebService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<BlazorWebServiceImplementation> _logger;

        public HttpClient _httpClient;

        public BlazorWebServiceImplementation(IConfiguration config, ILogger<BlazorWebServiceImplementation> logger, IHttpClientFactory clientFactory)
        {
            this._config = config;
            this._logger = logger;
            this._clientFactory = clientFactory;
        }

        public String WebServiceUrl { get { return this.GetURL(); } }

        private String GetAuthToken()
        {
            String tokenValue = Environment.GetEnvironmentVariable("AUTH_TOKEN");
            return tokenValue;
        }
        public String GetURL()
        {
            String webServiceUrl = _config.GetValue<string>("WEB_SERVICE_CONFIG:WEB_SERVICE_URL");
            return webServiceUrl;
        }

        private HttpRequestMessage getRequest(String url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            //request.Headers.Add("Authorization", "Bearer " + GetAuthToken());
            return request;
        }
        public async Task<TestComponentModel> GetMock()
        {
            // prepare the return object
            TestComponentModel testingComponent = new TestComponentModel();
            testingComponent.Title = "Mock DATA";
            testingComponent.Content = "This is just some Mock DATA";

            return testingComponent;
        }

            public async Task<TestComponentModel> Get()
        {
            // prepare the return object
            TestComponentModel testingComponent = new TestComponentModel();

            // 
            String url = this.GetURL();

            //  generate a new client
            var client = _clientFactory.CreateClient();

            //  get the request
            var request = this.getRequest(url);

            // execute the request and get the response
            //var response = await client.SendAsync(request);
            var response = client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                String jsonString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received string: " + jsonString);

                //  read the response stream
                using var responseStream = await response.Content.ReadAsStreamAsync();

                //  map the response to the return object
                try
                {
                    testingComponent = await JsonSerializer.DeserializeAsync<TestComponentModel>(responseStream);
                }
                catch
                {
                    // throw an error
                    throw new Exception("Failed to parse the API response");
                }
            }
            else
            {
                // throw an error
                throw new Exception("Failed to get the API response. Response code: " + response.StatusCode);
            }

            // return the object
            return testingComponent;
        }
    }
}
