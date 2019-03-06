using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace CreditCardGenerator.Extensions
{
    public static class HttpRequestExtensions
    {
        public async static Task<T> ReadAsAsync<T>(this HttpRequest request)
        {
            var json = await request.ReadAsStringAsync();
            T retValue = JsonConvert.DeserializeObject<T>(json);
            return retValue;
        }
    }
}