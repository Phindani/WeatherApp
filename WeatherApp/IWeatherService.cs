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
    public interface IWeatherService
    {
       Task<WeatherInfo.RootObject> GetWeather(string cityName);
    }
}