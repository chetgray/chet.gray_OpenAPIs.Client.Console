using System;
using System.Net.Http;

using OpenAPIs.Client.Console.Business.Zippopotamus;
using OpenAPIs.Client.Console.Models.Zippopotamus;

using static System.Console;

namespace OpenAPIs.Client.Console
{
    internal static class Program
    {
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
            Write("Enter a country code (e.g. GB, US, DE, etc.)\n» ");
            string countryInput = ReadLine();
            Write("Enter a postal code\n» ");
            string postcodeInput = ReadLine();
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
            Write("Enter a country code (e.g. GB, US, DE, etc.)\n» ");
            string countryInput2 = ReadLine();
            Write("Enter a state/province abbreviation\n» ");
            string stateInput = ReadLine();
            Write("Enter a place name\n» ");
            string placenameInput = ReadLine();
            WriteLine();

            PlacenameResultModel placenameResult;
            try
            {
                placenameResult = bl.QueryPlacenameAsync(
                    countryInput2,
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
                        + "[3] \n"
                        + "[4] \n"
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
                        break;

                    case "4":
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
