using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;
using SoftRes.Auth;
using SoftRes.Config;
using SoftRes.Models;

namespace SoftRes.BlizzardAPI.Items
{
    public class ItemQualityResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemQuality Name;
    }

    public class ItemResponse
    {
        public int Id;
        public string Name;
        public ItemQualityResponse Quality;
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
        private RestRequest _request;

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
                _request.AddParameter("access_token", token, ParameterType.QueryString);
                var response = await _client.ExecuteAsync(_request);
                
                if (response.IsSuccessful)
                {
                    var item = JsonConvert.DeserializeObject<ItemResponse>(response.Content);

                    return new Item
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Quality = item.Quality.Name
                    };
                }

                throw new Exception("Fetching item data failed.");
            }

            throw new ArgumentNullException("Access token cannot be null.");
        }

        public IItemAPINamespace ItemId(int id)
        {
            _request = new RestRequest(
                $"/data/wow/item/{id}",
                Method.GET
            );

            return this;
        }

        public IItemAPIExecute Locale(string locale)
        {
            if (!String.IsNullOrEmpty(locale))
            {
                _request.AddParameter(
                    new Parameter(
                        "locale",
                        locale,
                        ParameterType.QueryString
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
                _request.AddParameter(
                    new Parameter(
                        "namespace",
                        ns,
                        ParameterType.QueryString
                        )
                );

                return this;
            }

            throw new ArgumentNullException("Namespace string cannot be null.");
        }
    }

    public interface IItemAPIExecute
    {
        Task<Item> Execute();
    }

    public interface IItemAPINamespace
    {
        IItemAPIItemLocale Namespace(string ns);
    }

    public interface IItemAPIItemId
    {
        IItemAPINamespace ItemId(int id);
    }

    public interface IItemAPIItemLocale
    {
        IItemAPIExecute Locale(string locale);
    }
}