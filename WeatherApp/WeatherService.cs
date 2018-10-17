using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Net;
using Org.Json;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class WeatherService : IWeatherService
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather?q=";
        private const string UrlQueryString = "&units=metric&appid=dc2d8a0b75afc896aaa1aa6e9dce7952";
        private readonly HttpClient httpClient;

        public WeatherService()
        {
            httpClient = new HttpClient();
        }

        public async Task<WeatherInfo.RootObject> GetWeather(string cityName)
        {
            WeatherInfo.RootObject result = null;
            try
            {
                var url = BaseUrl + cityName + UrlQueryString;
                var json = await httpClient.GetAsync(url);
                if (json.IsSuccessStatusCode)
                {
                    var jsonContent = await json.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<WeatherInfo.RootObject>(jsonContent);
                }
            }
            catch (System.Exception) {
                Toast.MakeText(Application.Context, "Cannot load data", ToastLength.Short).Show();
            }
            return result;
        }
    }
}