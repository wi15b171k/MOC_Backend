
using Newtonsoft.Json;
using Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceCommunication
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Serice calls...");

            
            SrvCom com = new SrvCom();

            Console.WriteLine("Login...");
            var isAdmin = com.LoginAdmin("flo@flo.at", "123");

            Console.WriteLine("Get active Reports...");
            var reports = com.GetActiveReports();

            Console.WriteLine("Update Report with id=1...");
            var isUpdates = com.UpdateReport(true, 1);

            Console.WriteLine("Get Picture");
            var pic = com.GetPicture(1, 300, 200);

            Console.WriteLine("Add new Admin User");
            PersonAdminAddSM admin = new PersonAdminAddSM
            {
                Email = "admin@admin.com",
                Password = "123",
                ConfirmPassword = "123",
                FirstName = "Bernd",
                LastName = "Brot"
            };
            var isNewUser = com.AddNewAdminUser(admin);

            Console.WriteLine("Finished");

        }

        static void DemoCode()
        {
            //Set Up Client
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:48897/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", "flo@flo.at" ),
                        new KeyValuePair<string, string> ( "Password", "123" )
                    };

            var content = new FormUrlEncodedContent(pairs);

            //Post get beraer Token of user
            var response = client.PostAsync(client.BaseAddress + "token", content).Result;

            string jsonString = response.Content.ReadAsStringAsync().Result;
            object responseData = JsonConvert.DeserializeObject(jsonString);
            string accessToken = ((dynamic)responseData).access_token;

            //Add Bearer Token to Request Herader
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //get access type
            bool isAdmin = false;
            response = client.GetAsync(client.BaseAddress + "api/account/GetAdminState").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                isAdmin = true;
            }
            else
            {
                isAdmin = false;
            }

            Console.WriteLine("debug");


            //Get single Trip
            response = client.GetAsync(client.BaseAddress + "api/trip/2").Result;
            if (response.IsSuccessStatusCode)
            {
                var model = response.Content.ReadAsAsync<TripSM>().Result;
                Console.WriteLine("debug");
            }


            //get list of reports
            response = client.GetAsync(client.BaseAddress + "api/reports").Result;
            if (response.IsSuccessStatusCode)
            {

                var model = response.Content.ReadAsAsync<List<ReportSM>>().Result;
                Console.WriteLine("debug");
            }

            //Post new friend request
            response = client.PostAsJsonAsync(client.BaseAddress + "api/requests", "f8fccb01-ddc4-432e-abd5-f64f7812e1de").Result;


            //PUT report
            bool isForbidden = true;
            response = client.PutAsJsonAsync(client.BaseAddress + "api/reports/1", isForbidden).Result;


            //Post new Admin User
            PersonAdminAddSM admin = new PersonAdminAddSM
            {
                Email = "admin@admin.com",
                Password = "123",
                ConfirmPassword = "123",
                FirstName = "Bernd",
                LastName = "Brot"
            };

            response = client.PostAsJsonAsync(client.BaseAddress + "api/Account/RegisterAdmin", admin).Result;


            //Get Picture
            response = client.GetAsync(client.BaseAddress + "api/pic/2/0/0").Result;
            if (response.IsSuccessStatusCode)
            {
                //var model = response.Content.ReadAsAsync<TripSM>().Result;
                var pic = response.Content.ReadAsByteArrayAsync().Result;

                //save byte array to file
                //File.WriteAllBytes(@"c:\Photos\meines.jpg", pic);

                Console.WriteLine("debug");
            }



            Console.WriteLine("debug");

        }
    }
}
