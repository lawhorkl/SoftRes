using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using SoftRes.Auth;
using SoftRes.Models;

namespace SoftRes.BlizzardAPI
{
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