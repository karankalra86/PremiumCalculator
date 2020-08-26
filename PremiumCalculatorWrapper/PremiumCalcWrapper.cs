using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PremiumCalcModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PremiumCalculatorWrapper
{
    public class PremiumCalcWrapper : IPremiumCalcWrapper
    {
        public PremiumCalcWrapper(string strApiURL)
        {
            ApiURL = strApiURL;
        }

        public string ApiURL { get; set; }

        public Task<decimal> CalculatePremium(MemberModel objMemberModel)
        {
            var accessToken = string.Empty;
            using (var client = CreateClient(accessToken))
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(objMemberModel), Encoding.UTF8, "application/json");
                var response = client.PostAsync(ApiURL, content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return null;
                }
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                if (response.IsSuccessStatusCode)
                {
                    var calculatePremium = JsonConvert.DeserializeObject<decimal>(response.Content.ReadAsStringAsync().Result, serSettings);
                    return Task.FromResult(calculatePremium);
                }
                else
                    return Task.FromResult<decimal>(decimal.Zero);
            }
        }

        public Task<List<string>> GetOccupations()
        {
            var accessToken = string.Empty;
            using (var client = CreateClient(accessToken))
            {
                var response = client.GetAsync(ApiURL).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return null;
                }
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                if (response.IsSuccessStatusCode)
                {
                    var calculatePremium = JsonConvert.DeserializeObject<List<string>>(response.Content.ReadAsStringAsync().Result, serSettings);
                    return Task.FromResult(calculatePremium);
                }
                else
                    return Task.FromResult<List<string>>(null);
            }
        }

        public static HttpClient CreateClient(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
