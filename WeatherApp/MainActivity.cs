using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Views;
using Newtonsoft.Json;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText etCity;
        Button btnGetWeather;
        ProgressBar progressBar;
        string cityName = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            etCity = FindViewById<EditText>(Resource.Id.etCityName);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            btnGetWeather = FindViewById<Button>(Resource.Id.btnGetWeather);


            btnGetWeather.Click += BtnGetWeather_ClickAsync;

        }

        public async void BtnGetWeather_ClickAsync(object sender, System.EventArgs e)
        {
            cityName = etCity.Text.ToString();
            if (etCity.Text.ToString().Length > 0)
            {
                progressBar.Visibility = ViewStates.Visible;
                WeatherService weatherService = new WeatherService();
                var result = await weatherService.GetWeather(etCity.Text.ToString());
                //WeatherInfo.RootObject weatherInfo = new WeatherInfo.RootObject();
                //weatherInfo = result;
                if (result == null)
                {
                    progressBar.Visibility = ViewStates.Invisible;
                    Toast.MakeText(Application, "Cannot load data", ToastLength.Short).Show();
                }
                else
                {
                    progressBar.Visibility = ViewStates.Invisible;
                    Intent weather_details = new Intent(this, typeof(WeatherDetailsActivity));
                    weather_details.PutExtra("DATA", JsonConvert.SerializeObject(result));
                    StartActivity(weather_details);
                }
            }
            else
            {
                Toast.MakeText(Application, "Please type a city name", ToastLength.Short).Show();
            }
        }
    }
}
    
