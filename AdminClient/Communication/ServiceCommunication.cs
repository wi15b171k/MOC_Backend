using Newtonsoft.Json;
using Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.Communication
{
    public class ServiceCommunication
    {
        private static HttpClient client = new HttpClient();


        public ServiceCommunication()
        {
            
        }

        public bool InitClient()
        {
            //client.BaseAddress = new Uri("http://localhost:48897/");
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return true;
        }

        public bool LoginAdmin(string email, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", email),
                        new KeyValuePair<string, string> ("Password", password)
                    };

            var content = new FormUrlEncodedContent(pairs);

            //Post get beraer Token of user
            var response = client.PostAsync(client.BaseAddress + "token", content).Result;

            string jsonString = response.Content.ReadAsStringAsync().Result;
            object responseData = JsonConvert.DeserializeObject(jsonString);
            string accessToken = ((dynamic)responseData).access_token;

            //Add Bearer Token to Request Herader
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


            //check if user is admin
            if(CheckIfAdmin() == true)
            {
                return true;
            }else
            {
                return false;
            }
        }

        private bool CheckIfAdmin()
        {
            var response = client.GetAsync(client.BaseAddress + "api/account/GetAdminState").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ReportSM> GetActiveReports()
        {
            //get list of reports
            var response = client.GetAsync(client.BaseAddress + "api/reports").Result;
            if (response.IsSuccessStatusCode)
            {

                var reports = response.Content.ReadAsAsync<List<ReportSM>>().Result;
                return reports;
            }else
            {
                return null;
            }
        }


        public bool UpdateReport(bool isForbidden, int id)
        {
            var response = client.PutAsJsonAsync(client.BaseAddress + "api/reports/" + id, isForbidden).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }else
            {
                return false;
            }
        }


        public byte[] GetPicture(int id, int width, int height)
        {
            //Get Picture
            var response = client.GetAsync(client.BaseAddress + "api/pic/" + id + "/" + width + "/" + height).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var pic = response.Content.ReadAsByteArrayAsync().Result;
                return pic;
            }else
            {
                return null;
            }
        }


        public bool AddNewAdminUser(PersonAdminAddSM admin)
        {
            var response = client.PostAsJsonAsync(client.BaseAddress + "api/Account/RegisterAdmin", admin).Result;
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
