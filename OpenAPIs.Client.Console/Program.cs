using System;
using System.Net.Http;

using OpenAPIs.Client.Console.Business.Zippopotamus;
using OpenAPIs.Client.Console.Models.Zippopotamus;

using static System.Console;

namespace OpenAPIs.Client.Console
{
    internal static class Program
    {
        private static void Main()
        {
            ApiHelper.InitializeClient();
            IZippopotamusBL zippopotamusBL = new ZippopotamusBL(ApiHelper.ApiClient);

            bool shouldContinue = true;
            while (shouldContinue)
            {
                WriteLine(
                    "Available API query options:\n"
                        + "[1] Look up place by post code\n"
                        + "[2] Look up post codes by place name\n"
                        + "[3] \n"
                        + "[4] \n"
                        + "[5] \n"
                        + "[0] Exit\n"
                );
                Write("Which API would you like to call?\n» ");
                string menuInput = ReadLine();
                WriteLine();

                string countryInput;
                switch (menuInput)
                {
                    case "1":
                        // Look up place by post code
                        Write("Enter a country code (e.g. GB, US, DE, etc.):\n» ");
                        countryInput = ReadLine();
                        Write("Enter a postal code:\n» ");
                        string postcodeInput = ReadLine();
                        WriteLine();

                        PostcodeResultModel postcodeResult;
                        try
                        {
                            postcodeResult = zippopotamusBL
                                .QueryPostcodeAsync(countryInput, postcodeInput)
                                .Result;
                        }
                        catch (AggregateException aggEx)
                        {
                            aggEx
                                .Flatten()
                                .Handle(e =>
                                {
                                    if (e is HttpRequestException)
                                    {
                                        WriteLine($"ERROR: {e.Message}");
                                    }
                                    return e is HttpRequestException;
                                });
                            break;
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
                        break;

                    case "2":
                        // Look up post codes by place name
                        Write("Enter a country code (e.g. GB, US, DE, etc.):\n» ");
                        countryInput = ReadLine();
                        Write("Enter a state/province abbreviation:\n» ");
                        string stateInput = ReadLine();
                        Write("Enter a place name:\n» ");
                        string placenameInput = ReadLine();
                        WriteLine();

                        PlacenameResultModel placenameResult;
                        try
                        {
                            placenameResult = zippopotamusBL
                                .QueryPlacenameAsync(countryInput, stateInput, placenameInput)
                                .Result;
                        }
                        catch (AggregateException aggEx)
                        {
                            aggEx
                                .Flatten()
                                .Handle(e =>
                                {
                                    if (e is HttpRequestException)
                                    {
                                        WriteLine($"ERROR: {e.Message}");
                                    }
                                    return e is HttpRequestException;
                                });
                            break;
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
