using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using MauiProjectBWeather.Models;
using MauiProjectBWeather.Services;

namespace MauiProjectBWeather.Views
{
    public class GroupedForecast
    {
        public string City { get; set; }
        public IEnumerable<IGrouping<DateTime, ForecastItem>> Items { get; set; }
    }

    public partial class ForecastPage : ContentPage
    {
        private OpenWeatherService _service;
        private CityPicture _city;

        public ForecastPage(CityPicture city)
        {
            InitializeComponent();
 
            _city = city;
            _service = new OpenWeatherService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = $"Forecast for {_city.Name}";

            MainThread.BeginInvokeOnMainThread(async () => {await LoadForecast();});
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await LoadForecast();
        }

        private async Task LoadForecast()
        {
            Forecast forecast = await _service.GetForecastAsync(_city.Name);
        }
    }
}