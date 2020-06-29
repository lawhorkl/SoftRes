using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using SoftRes.Auth;
using SoftRes.Config;
using SoftRes.Models;

namespace SoftRes.BlizzardAPI
{
    public class ItemResponse
    {
        public int Id;
        public string Name;
    }

    public interface IBlizzardItemAPI : IItemAPIItemId {}

    public class BlizzardItemAPI : 
        IBlizzardItemAPI,
        IItemAPIItemLocale, 
        IItemAPINamespace, 
        IItemAPIExecute
    {
        private readonly IBlizzardAuthHandler _handler;
        private RestClient _client;
        private List<Parameter> _parameters = new List<Parameter>();
        private int _itemId = 0;

        public BlizzardItemAPI(IBlizzardAuthHandler handler, ApplicationConfig config)
        {
            _handler = handler;
            _client = new RestClient(config.GameDataAPI.Uri);
        }

        public async Task<Item> Execute()
        {
            var token = await _handler.AccessToken();
            if (!String.IsNullOrEmpty(token))
            {
                var request = new RestRequest(
                $"/data/wow/item/{_itemId}", 
                Method.GET
                );

                foreach (var parameter in _parameters)
                {
                    request.AddParameter(parameter);
                }

                request.AddParameter("access_token", token);

                var response = await _client.ExecuteAsync(request);
                
                if (response.IsSuccessful)
                {
                    var item = JsonConvert.DeserializeObject<ItemResponse>(response.Content);

                    return new Item
                    {
                        Id = item.Id
                    };
                }

                throw new Exception("Fetching item data failed.");
            }

            throw new ArgumentNullException("Access token cannot be null.");
        }

        public IItemAPINamespace ItemId(int id)
        {
            _itemId = id;

            return this;
        }

        public IItemAPIExecute Locale(string locale)
        {
            if (!String.IsNullOrEmpty(locale))
            {
                _parameters.Add(
                    new Parameter(
                        "locale", 
                        locale, 
                        ParameterType.UrlSegment
                    )
                );

                return this;
            }

            throw new ArgumentNullException("Locale string cannot be null.");
        }

        public IItemAPIItemLocale Namespace(string ns)
        {
            if (!String.IsNullOrEmpty(ns))
            {
                _parameters.Add(
                    new Parameter(
                        "namespace", 
                        ns, 
                        ParameterType.UrlSegment
                        )
                );

                return this;
            }

            throw new ArgumentNullException("Namespace string cannot be null.");
        }
    }
}