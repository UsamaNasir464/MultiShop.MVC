using System.Threading.Tasks;

namespace MultiShop.Mvc.Utills
{
    public interface IApiCall
    {
        Task<T> CallApiGetAsync<T>(string apiPath) where T : new();
        Task LogoutAsync(string apiPath);
        Task<string> CallUserIdApiGetAsync(string apiPath);
        Task<T> CallApiPostAsync<T>(string apiPath, T postData) where T :  new();
        Task<bool> CallApiDeleteAsync(string apiPath);
        Task<T> CallApiPostAsync<T, X>(string apiPath, X postData) where T : new() where X : new();
        Task<bool> GetBoolAsync(string apiPath);
    }
}
