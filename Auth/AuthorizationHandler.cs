using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using SoftRes.Config;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;

namespace SoftRes.Auth
{
    public class AccessTokenResponse
    {
        public string AccessToken;
        public string TokenType;
        public int ExpiresIn;
    }

    public interface IBlizzardAuthHandler
    {
        Task<string> AccessToken();
    }
    
    public class BlizzardAuthHandler : IBlizzardAuthHandler
    {
        private AuthConfig _config;
        private IHttpClientFactory _clientFactory;
        private string _accessToken;
        private int _expires;

        public BlizzardAuthHandler(
            AuthConfig config,
            IHttpClientFactory clientFactory
        )
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task<string> AccessToken()
        {
            if (String.IsNullOrEmpty(_accessToken))
            {
                var client = new RestClient(_config.Uri);
                client.Authenticator = new HttpBasicAuthenticator(_config.ClientId, _config.ClientSecret);
                var request = new RestRequest(Method.POST);
                var basicAuth = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        $"{_config.ClientId}:{_config.ClientSecret}"
                    )
                );
                var headers = new List<(string, string)>
                {
                    ("cache-control", "no-cache"),
                    ("content-type", "application/x-www-form-urlencoded"),
                };

                foreach (var header in headers)
                {
                    request.AddHeader(header.Item1, header.Item2);
                }

                request.AddParameter(
                    "grant_type", 
                    "client_credentials"
                );

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var deserializedResonse = 
                        JsonConvert.DeserializeObject<AccessTokenResponse>(response.Content);

                    _expires = deserializedResonse.ExpiresIn;
                    _accessToken = deserializedResonse.AccessToken;
                }
            }

            return _accessToken;
        }
    }
}