﻿using System;

namespace MauiProjectBWeather.Models
{
    public class ForecastItem
    {
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public override string ToString() => $"{Description}, temperature: {Temperature} degC, wind: {WindSpeed} m/s";
    }
}
