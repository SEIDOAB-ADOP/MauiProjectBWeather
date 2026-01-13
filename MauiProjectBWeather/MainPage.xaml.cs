using MauiProjectBWeather.Services;

namespace MauiProjectBWeather
{
    public partial class MainPage : ContentPage
    {
        private OpenWeatherService _service;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            _service = new OpenWeatherService();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                var forecast = await _service.GetForecastAsync("Stockholm");
                ServiceLabel.Text = $"{forecast.Items.Count} forcast items read.";
            }
            catch (Exception ex)
            {
                ServiceLabel.Text = $"Error reading forecast: {ex.Message}";
            }
        }


        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
