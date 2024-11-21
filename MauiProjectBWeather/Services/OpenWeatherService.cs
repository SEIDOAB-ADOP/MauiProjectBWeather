﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

using MauiProjectBWeather.Models;

namespace MauiProjectBWeather.Services
{
    public class OpenWeatherService
    {
        readonly HttpClient _httpClient = new HttpClient();
        readonly string _apiKey = "";
        readonly string _apiUri = "https://api.openweathermap.org/data/2.5/forecast";

        public async Task<Forecast> GetForecastAsync(string City)
        {
            //https://openweathermap.org/current
            var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var uri = $"{_apiUri}?q={City}&units=metric&lang={language}&appid={_apiKey}";

            Forecast forecast = await ReadWebApiAsync(uri);
            return forecast;
        }
        public async Task<Forecast> GetForecastAsync(double latitude, double longitude)
        {
            //https://openweathermap.org/current
            var language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            var uri = $"{_apiUri}?lat={latitude}&lon={longitude}&units=metric&lang={language}&appid={_apiKey}";

            Forecast forecast = await ReadWebApiAsync(uri);
            return forecast;
        }
        private async Task<Forecast> ReadWebApiAsync(string uri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            WeatherApiData wd = await response.Content.ReadFromJsonAsync<WeatherApiData>();

            var forecast = new Forecast()
            {
                City = wd.city.name,
                Items = wd.list.Select(wdle => new ForecastItem()
                {
                    DateTime = UnixTimeStampToDateTime(wdle.dt),
                    Temperature = wdle.main.temp,
                    WindSpeed = wdle.wind.speed,
                    Description = wdle.weather.First().description,
                    Icon = $"https://openweathermap.org/img/w/{wdle.weather.First().icon}.png"
                }).ToList()
            };
            return forecast;
        }
        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
