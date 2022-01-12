using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientWrapper
{
    public class HttpClientWrapperClient : HttpClient, IHttpClientWrapperClient
    {
        public HttpClientWrapperClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public async Task<JObject> GetJObjectAsync(String url)
        {
            try
            {
                using (HttpResponseMessage response = await this.GetAsync(url))
                {
                    return await HandleResponse(response);
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public async Task<JObject> PutJsonAsync<T>(String url, T obj)
        {

            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.None);
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    using (HttpResponseMessage response = await this.PutAsync(url, content))
                    {
                        return await HandleResponse(response);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<JObject> PostJsonAsync<T>(String url, T obj)
        {

            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.None);
                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    using (HttpResponseMessage response = await this.PostAsync(url, content))
                    {
                        return await HandleResponse(response);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        private static async Task<JObject> HandleResponse(HttpResponseMessage response)
        {
            try
            {
                JObject result;
                using (var reader = new JsonTextReader(new StreamReader(await response.Content.ReadAsStreamAsync())))
                {
                    result = JObject.Load(reader);
                }

                if (!response.IsSuccessStatusCode)
                {
                    if (result.TryGetValue("message", out JToken message))
                    {
                        //If we have a message use it.
                        throw new Exception($"Error from http request: {message.Value<String>()}");
                    }
                    else
                    {
                        try
                        {
                            response.EnsureSuccessStatusCode(); //Let default handler fire otherwise
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"A {ex.GetType().Name} occured loading data from AzureDevops. Message: {ex.Message}", ex);
                        }
                    }
                }

                return result;
            }
            catch (JsonReaderException ex)
            {
                throw new Exception($"Error parsing json from http request. Http Status: {response.StatusCode}. {ex.GetType().Name} Message: {ex.Message}", ex);
            }
        }
    }


}
