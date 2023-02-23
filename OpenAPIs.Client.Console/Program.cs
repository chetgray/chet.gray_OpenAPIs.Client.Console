using static System.Console;

namespace OpenAPIs.Client.Console
{
    internal static class Program
    {
        private static void Main()
        {
            ApiHelper.InitializeClient();

            bool shouldContinue = true;
            while (shouldContinue)
            {
                WriteLine(
                    "[1] Lookup place by post code\n"
                        + "[2] Look up post codes by place\n"
                        + "[3] \n"
                        + "[4] \n"
                        + "[5] \n"
                        + "[0] Exit\n"
                );
                Write("Which API would you like to call?\n» ");
                string menuInput = ReadLine();
                switch (menuInput)
                {
                    case "1":
                        break;

                    case "2":
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
                Write("Would you like to make another choice? ([y]/n)\n» ");
                string continueInput = ReadLine();
                if (continueInput.StartsWith("n", ignoreCase: true, culture: null))
                {
                    shouldContinue = false;
                }
            }
            WriteLine("\nGoodbye!\n");
            WriteLine("Press any key to exit . . . ");
            ReadKey(intercept: true);
        }
    }
}
