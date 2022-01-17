using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace HttpClientWrapper
{
    public interface IHttpClientWrapperClient
    {
        Task<JObject> GetJObjectAsync(String url);

        Task<JObject> PutJsonAsync<T>(String url, T obj);

        Task<JObject> PostJsonAsync<T>(String url, T obj);

    }
}
