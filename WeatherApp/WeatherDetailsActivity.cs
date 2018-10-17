using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Sql;
using Newtonsoft.Json;
using Square.Picasso;
using Environment = System.Environment;

namespace WeatherApp
{
    [Activity(Label = "WeatherDetailsActivity")]
    public class WeatherDetailsActivity : Activity
    {
        TextView txtCityName;
        TextView txtTemperature;
        TextView txtDateTime;
        ImageView imageView;
        ListView lvTable;
        List<string> myListItems;

        string city = "";
        string country = "";
        double temperature;
        string description = "";
        string icon = "";
        readonly string iconUrl = "http://openweathermap.org/img/w/";
        string date_time;
        string windInfo = "";
        string humidity = "";
        string pressure = "";
        string GeoCoords = "";
        string cloudiness = "";
        string sunrise;
        string sunset;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "activity_weather_details" layout resource
            SetContentView(Resource.Layout.activity_weather_details);

            txtCityName = FindViewById<TextView>(Resource.Id.tvCityName);
            txtTemperature = FindViewById<TextView>(Resource.Id.tvTemperature);
            txtDateTime = FindViewById<TextView>(Resource.Id.tvDateTime);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            lvTable = FindViewById<ListView>(Resource.Id.lvTable);
            var result = JsonConvert.DeserializeObject<WeatherInfo.RootObject>(Intent.GetStringExtra("DATA"));

            try
            {
                city = result.name;
                country = result.sys.country;
                temperature =Math.Round(result.main.temp,1);
                description = result.weather[0].description;
                icon = result.weather[0].icon;

                Date dt = new Date(Convert.ToInt64(result.dt) * 1000);
                SimpleDateFormat sfd = new SimpleDateFormat("yyyy/MM/dd HH:mm");
                date_time = sfd.Format(dt).ToString();
                windInfo = result.wind.speed + " m/s" + Environment.NewLine +
                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t" + result.wind.deg.ToString("0.00") + " degrees";
                cloudiness = result.clouds.all + " %";
                pressure = result.main.pressure + " hpa";
                humidity = result.main.humidity + " %";
                Date dtSunrise = new Date(Convert.ToInt64(result.sys.sunrise) * 1000);
                SimpleDateFormat sfdSunrise = new SimpleDateFormat("HH:mm");
                sunrise = sfdSunrise.Format(dtSunrise).ToString();
                Date dtSunset = new Date(Convert.ToInt64(result.sys.sunset) * 1000);
                SimpleDateFormat sfdSunset = new SimpleDateFormat("HH:mm");
                sunset = sfdSunset.Format(dtSunset).ToString();
                GeoCoords = "[" + result.coord.lat + ", " + result.coord.lon + "]";

                txtCityName.Text = "Weather in " + city + ", " + country;
                Picasso.With(this).Load(iconUrl + icon + ".png").Into(imageView);
                txtTemperature.Text = temperature + " \u2103";
                txtDateTime.Text = description + "\n" + "Updated at " + date_time;
                myListItems = new List<string>
                {
                "Wind" + "\t\t\t\t\t\t\t\t\t\t" + windInfo,
                "Cloudiness" + "\t\t\t\t" + cloudiness,
                "Pressure" + "\t\t\t\t\t\t" + pressure,
                "Humidity" + "\t\t\t\t\t\t" + humidity,
                "Sunrise" + "\t\t\t\t\t\t\t" + sunrise,
                "Sunset" + "\t\t\t\t\t\t\t\t" + sunset,
                "Geo coords" + "\t\t\t" + GeoCoords
                };
               
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, myListItems);
                lvTable.Adapter = adapter;
            }
            catch (JsonException)
            {
                Toast.MakeText(Application, "There was a problem with the data received from the server", ToastLength.Short).Show();
            }
        }
    }
}