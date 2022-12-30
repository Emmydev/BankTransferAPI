using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paystack.Response;
using RestSharp;
using System.Configuration;
using System.Net;

namespace Paystack
{
    public class PaystackService: IPaystackService
    {
        private readonly RestClient client = new RestClient("https://api.paystack.co/");
        private readonly string apiKey = "YOUR_SECRET_KEY";
        public async Task<BankList> GetPaystackBankList()
        {
            try
            {
                var request = new RestRequest($"bank?currency=NGN", Method.Get);
                request.AddHeader("Authorization", $"Bearer {apiKey}");
                RestResponse response = await client.ExecuteAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //>>> log error
                    return null;
                }

                var bankList = JsonConvert.DeserializeObject<BankList>(response.Content);
                if (bankList == null)
                {
                    //>>> log error
                    return null;
                }

                if (!bankList.Status)
                {
                    // log error
                    return null;
                }

                return bankList;

            }
            catch (Exception ex)
            {
                //>>> log Error
                return null;
            }
        }
    }

}