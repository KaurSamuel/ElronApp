using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace StarWarsApi
{
    class ApiSecvice
    {
        public static async Task<dynamic> Get_Single(string Query)
        {
            string url = "https://swapi.co/api/";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url+ Query+ "?format=json");

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
    }   
}