using System;
using System.Net.Http;

using OpenAPIs.Client.Console.Business.SevenTimer;
using OpenAPIs.Client.Console.Business.SunriseSunset;
using OpenAPIs.Client.Console.Business.Zippopotamus;
using OpenAPIs.Client.Console.Models.SevenTimer;
using OpenAPIs.Client.Console.Models.SunriseSunset;
using OpenAPIs.Client.Console.Models.Zippopotamus;

using static System.Console;

namespace OpenAPIs.Client.Console
{
    internal static class Program
    {
        private static readonly ISevenTimerBL _sevenTimerBL = new SevenTimerBL(
            ApiHelper.ApiClient
        );
        private static readonly ISunriseSunsetBL _sunriseSunsetBL = new SunriseSunsetBL(
            ApiHelper.ApiClient
        );
        private static readonly IZippopotamusBL _zippopotamusBL = new ZippopotamusBL(
            ApiHelper.ApiClient
        );

        /// <summary>
        /// Handles exceptions thrown by the API BL classes.
        /// </summary>
        /// <param name="ex">The exception to handle.</param>
        /// <returns>
        /// <see langword="true">true</see> if the exception was handled; otherwise, <see
        /// langword="false">false</see>.
        /// </returns>
        /// <remarks>
        /// This method is passed as a predicate to <see
        /// cref="AggregateException.Handle">AggregateException.Handle</see>. For <see
        /// cref="HttpRequestException">HttpRequestException</see>s, it writes the <see
        /// cref="Exception.Message">message</see> to the console. Other <see
        /// cref="Exception">exception</see>s are not handled.
        /// </remarks>
        private static bool HandleApiExceptions(Exception ex)
        {
            if (ex is HttpRequestException)
            {
                WriteLine($"ERROR: {ex.Message}");
            }
            return ex is HttpRequestException;
        }

        /// <summary>
        /// Prompts the user to enter a country code and postal code, then queries the
        /// Zippopotamus API for the corresponding places and writes them to the console.
        /// </summary>
        private static void LookUpPlacesByPostCode(IZippopotamusBL bl)
        {
            string countryInput = null;
            while (string.IsNullOrWhiteSpace(countryInput))
            {
                Write("Enter a country code (e.g. GB, US, DE, etc.)\n» ");
                countryInput = ReadLine();
            }
            string postcodeInput = null;
            while (string.IsNullOrWhiteSpace(postcodeInput))
            {
                Write("Enter a postal code\n» ");
                postcodeInput = ReadLine();
            }
            WriteLine();

            PostcodeResultModel postcodeResult;
            try
            {
                postcodeResult = bl.QueryPostcodeAsync(countryInput, postcodeInput).Result;
            }
            catch (AggregateException aggEx)
            {
                aggEx.Flatten().Handle(HandleApiExceptions);
                return;
            }
            WriteLine(
                $"Country:   {postcodeResult.Country} ({postcodeResult.CountryAbbreviation})\n"
                    + $"Post code: {postcodeResult.Postcode}"
            );
            foreach (PostcodeResultPlace place in postcodeResult.Places)
            {
                WriteLine(
                    "\n"
                        + $"Place name:    {place.Placename}\n"
                        + $"    State:     {place.State} ({place.StateAbbreviation})\n"
                        + $"    Latitude:  {place.Latitude}\n"
                        + $"    Longitude: {place.Longitude}"
                );
            }
        }

        /// <summary>
        /// Prompts the user to enter a country code, state/province abbreviation, and place
        /// name, then queries the Zippopotamus API for the corresponding postal codes and
        /// writes them to the console.
        /// </summary>
        private static void LookUpPostCodesByPlaceName(IZippopotamusBL bl)
        {
            string countryInput = null;
            while (string.IsNullOrWhiteSpace(countryInput))
            {
                Write("Enter a country code (e.g. GB, US, DE, etc.)\n» ");
                countryInput = ReadLine();
            }
            string stateInput = null;
            while (string.IsNullOrWhiteSpace(stateInput))
            {
                Write("Enter a state/province abbreviation\n» ");
                stateInput = ReadLine();
            }
            string placenameInput = null;
            while (string.IsNullOrWhiteSpace(placenameInput))
            {
                Write("Enter a place name\n» ");
                placenameInput = ReadLine();
            }
            WriteLine();

            PlacenameResultModel placenameResult;
            try
            {
                placenameResult = bl.QueryPlacenameAsync(
                    countryInput,
                    stateInput,
                    placenameInput
                ).Result;
            }
            catch (AggregateException aggEx)
            {
                aggEx.Flatten().Handle(HandleApiExceptions);
                return;
            }
            WriteLine(
                $"Country: {placenameResult.Country} ({placenameResult.CountryAbbreviation})\n"
                    + $"State:   {placenameResult.State} ({placenameResult.StateAbbreviation})"
            );
            foreach (PlacenameResultPlace place in placenameResult.Places)
            {
                WriteLine(
                    "\n"
                        + $"Postal code:    {place.Postcode}\n"
                        + $"    Place name: {place.Placename}\n"
                        + $"    Latitude:   {place.Latitude}\n"
                        + $"    Longitude:  {place.Longitude}"
                );
            }
        }

        /// <summary>
        /// Prompts the user to enter a latitude, a longitude, an optional date, and an
        /// optional time zone, then queries the Sunrise Sunset API for the corresponding
        /// sunrise and sunset times and writes them to the console.
        /// </summary>
        private static void LookUpSunriseSunset(ISunriseSunsetBL sunriseSunsetBL)
        {
            Write("Enter a latitude:\n» ");
            double latitude;
            while (!double.TryParse(ReadLine(), out latitude))
            {
                WriteLine("ERROR: Invalid latitude format.");
                Write("Enter a latitude:\n» ");
            }
            Write("Enter a longitude:\n» ");
            double longitude;
            while (!double.TryParse(ReadLine(), out longitude))
            {
                WriteLine("ERROR: Invalid longitude format.");
                Write("Enter a longitude:\n» ");
            }
            string date = null;
            bool isValidDate = false;
            while (!isValidDate)
            {
                Write(
                    "(Optional) Enter a date, or \"today\" or \"tomorrow\" (default is today):\n» "
                );
                string dateInput = ReadLine();
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    isValidDate = true;
                }
                else if (
                    dateInput.Equals("today", StringComparison.OrdinalIgnoreCase)
                    || dateInput.Equals("tomorrow", StringComparison.OrdinalIgnoreCase)
                )
                {
                    date = dateInput;
                    isValidDate = true;
                }
                else if (DateTime.TryParse(dateInput, out DateTime dateValue))
                {
                    date = dateValue.ToString("yyyy-MM-dd");
                    isValidDate = true;
                }
                else
                {
                    WriteLine("ERROR: Invalid date format.");
                }
            }
            TimeZoneInfo timeZone = null;
            bool isValidTimeZone = false;
            while (!isValidTimeZone)
            {
                Write(
                    "(Optional) Enter a time zone (default is the location's time zone):\n» "
                );
                string timeZoneInput = ReadLine();
                if (string.IsNullOrWhiteSpace(timeZoneInput))
                {
                    isValidTimeZone = true;
                }
                else
                {
                    try
                    {
                        timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneInput);
                        isValidTimeZone = true;
                    }
                    catch (TimeZoneNotFoundException ex)
                    {
                        WriteLine($"ERROR: {ex.Message}");
                    }
                }
            }
            WriteLine();

            SunriseSunsetResultModel sunriseSunsetResult;
            try
            {
                sunriseSunsetResult = sunriseSunsetBL
                    .QuerySunriseSunsetAsync(latitude, longitude, date, timeZone)
                    .Result;
            }
            catch (AggregateException aggEx)
            {
                aggEx.Flatten().Handle(HandleApiExceptions);
                return;
            }
            WriteLine(
                $"Sunrise:     {sunriseSunsetResult.Results.Sunrise:T}\n"
                    + $"Sunset:      {sunriseSunsetResult.Results.Sunset:T}\n"
                    + $"First Light: {sunriseSunsetResult.Results.FirstLight:T}\n"
                    + $"Last Light:  {sunriseSunsetResult.Results.LastLight:T}\n"
                    + $"Dawn:        {sunriseSunsetResult.Results.Dawn:T}\n"
                    + $"Dusk:        {sunriseSunsetResult.Results.Dusk:T}\n"
                    + $"Solar Noon:  {sunriseSunsetResult.Results.SolarNoon:T}\n"
                    + $"Golden Hour: {sunriseSunsetResult.Results.GoldenHour:T}\n"
                    + $"Day Length:  {sunriseSunsetResult.Results.DayLength}\n"
                    + $"Time Zone:   {sunriseSunsetResult.Results.TimeZone}"
            );
        }

        /// <summary>
        /// Prompts the user to enter a latitude and a longitude, then queries the 7Timer! API
        /// for the corresponding weather forecast and writes it to the console.
        /// </summary>
        private static void LookUpWeatherForecast(ISevenTimerBL sevenTimerBL)
        {
            Write("Enter a latitude:\n» ");
            double latitude;
            while (!double.TryParse(ReadLine(), out latitude))
            {
                WriteLine("ERROR: Invalid latitude format.");
                Write("Enter a latitude:\n» ");
            }
            Write("Enter a longitude:\n» ");
            double longitude;
            while (!double.TryParse(ReadLine(), out longitude))
            {
                WriteLine("ERROR: Invalid longitude format.");
                Write("Enter a longitude:\n» ");
            }
            WriteLine();

            ForecastResultModel forecastResult;
            try
            {
                forecastResult = sevenTimerBL
                    .QueryAstroForecastAsync(latitude, longitude)
                    .Result;
            }
            catch (AggregateException aggEx)
            {
                aggEx.Flatten().Handle(HandleApiExceptions);
                return;
            }
            WriteLine($"Forecast initialized: {forecastResult.InitialDateTime:g}");
            WriteLine(
                string.Format(
                    "{0,-18}{1,-14}{2,-8}{3,-11}{4,-10}{5,-31}",
                    "Date & Time",
                    "Cloud Cover",
                    "Temp.",
                    "Humidity",
                    "Precip.",
                    "Wind"
                )
            );
            foreach (ForecastDataPoint dataPoint in forecastResult.DataPoints)
            {
                WriteLine(
                    string.Format(
                        "{0,-18:ddd M/d HH:mm}{1,-14}{2,-8}{3,-11}{4,-10}{5,-31}",
                        forecastResult.InitialDateTime
                            + TimeSpan.FromHours(dataPoint.TimeOffsetHours),
                        ForecastResultModel.CloudCoverDescriptions[dataPoint.CloudCover],
                        $"{dataPoint.Temperature2M}°C",
                        ForecastResultModel.HumidityDescriptions[dataPoint.Humidity2M],
                        dataPoint.PrecipitationType,
                        $"{ForecastResultModel.WindSpeedCategories[dataPoint.Wind10M.Speed]}"
                            + $" ({dataPoint.Wind10M.Direction}"
                            + $" {ForecastResultModel.WindSpeedDescriptions[dataPoint.Wind10M.Speed]})"
                    )
                );
            }
        }

        /// <summary>
        /// The entry point of the application.
        /// </summary>
        private static void Main()
        {
            ApiHelper.InitializeClient();

            bool shouldContinue = true;
            while (shouldContinue)
            {
                WriteLine(
                    "Available API query options:\n"
                        + "[1] Look up places by post code\n"
                        + "[2] Look up post codes by place name\n"
                        + "[3] Look up sunrise and sunset times by latitude and longitude\n"
                        + "[4] Look up weather forecast by latitude and longitude\n"
                        + "[5] \n"
                        + "[0] Exit\n"
                );
                Write("Which API would you like to call?\n» ");
                string menuInput = ReadLine();
                WriteLine();

                switch (menuInput)
                {
                    case "1":
                        LookUpPlacesByPostCode(_zippopotamusBL);
                        break;

                    case "2":
                        LookUpPostCodesByPlaceName(_zippopotamusBL);
                        break;

                    case "3":
                        LookUpSunriseSunset(_sunriseSunsetBL);
                        break;

                    case "4":
                        LookUpWeatherForecast(_sevenTimerBL);
                        break;

                    case "5":
                        break;

                    case "0":
                        shouldContinue = false;
                        break;

                    default:
                        WriteLine("Invalid choice");
                        continue;
                }
                if (!shouldContinue)
                {
                    break;
                }
                WriteLine();

                Write("Would you like to make another choice? ([y]/n)\n» ");
                string continueInput = ReadLine();
                WriteLine();

                if (continueInput.StartsWith("n", ignoreCase: true, culture: null))
                {
                    shouldContinue = false;
                }
            }
            WriteLine("Goodbye!\n");

            WriteLine("Press any key to exit . . . ");
            ReadKey(intercept: true);
        }
    }
}
