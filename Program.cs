using System;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace Advania_Rebus // A bit over-engineered just for the fun of it, and to make it a bit more "user friendly". :')
{
    class Program
    {
        /*
         * Original job post: https://jobb.advania.se/jobs/1246209-utvecklare
         * Raw code reference file: https://advania.se/library/Images/01.-Sweden/3.-Dokument/Advaniarebus.txt
         */

        static void Main(string[] args)
        {
            const int multiplier = 2;
            const string input = "asdeelfkfÃ¶fkfjalfderkÃ¶kÃ¤kjds"; // Ã¥ = å, Ã¤ = ä, Ã¶ = ö
            //const string input = "asdeelfkföfkfjalfderkökäkjds";

            const string encodeFrom = "iso-8859-1";
            var inputDecoded = DecodeInput(input, encodeFrom);

            var output = "";
            var magicNumber = Math.Floor(Math.PI * Math.Ceiling(Math.PI)); // 3,145 * (Roof of 3,145=4) = 12,56, rounded down = 12. Created a variable for better overview.

            //for (int i = default; i < Math.Floor(Math.PI * Math.Ceiling(Math.PI)); i++) // Break out Math part to own variable!
            for (int i = default; i < magicNumber; i++)
            {
                if (IsPrimeNumber(i))
                {
                    Console.WriteLine("{0} is a Prime number.", i);
                    output += inputDecoded[i * multiplier];
                }
            }

            string urlToNavigateTo = @$"https://jobb.advania.se/jobs/1107089-vad-sags-om-en-digital-{string.Join("", output.Reverse())}-vi-vill-lara-kanna-dig/";

            string choice = string.Empty;

            static void ChooseAction(string choice, string urlToNavigateTo)
            {
                Console.WriteLine(
                    "\nWould you like to launch the default web browser and navigate to the following URL?" +
                    "\n{0}" +
                    "\n\nYES (opens web browser) / NO (exits the program): ", urlToNavigateTo);
                choice = Console.ReadLine().Trim();
                HandleChoice(choice, urlToNavigateTo);
            }

            //HandleChoice(choice, urlToNavigateTo);
            ChooseAction(choice, urlToNavigateTo);

            static string HandleChoice(string choice, string urlToNavigateTo)
            {
                choice = choice.ToUpper().Trim();
                urlToNavigateTo = urlToNavigateTo.Trim();

                switch (choice)
                    {
                    case "YES":
                        NavigateTo(urlToNavigateTo);
                        return choice;
                        //break;
                    case "NO":
                        return choice;
                    default:
                        ChooseAction(choice, urlToNavigateTo);
                        return choice;
                }
            }
          
            static string DecodeInput(string input, string encodeFrom)
            {
                var inputEncoding = Encoding.GetEncoding(encodeFrom); // Gets and sets the desired encoding from variable "encodeFrom".
                var text = inputEncoding.GetBytes(input); // Takes input string and turns it into Bytes for further re-encoding.
                var inputDecoded = Encoding.UTF8.GetString(text); // Encode the Byte array into an actual string with UTF8 encoding.

                return inputDecoded; // Returns the value of processed/encoded string.
            }

            static bool IsPrimeNumber(int i) // It's not too pretty, but it works for the set task at least... :'D
            {
                if (i >= 2)
                {
                    if (i == 2 || i == 3)
                    {
                        return true;
                    }
                    else if (i % 2 == 0 || i % 3 == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            static bool IsNullOrWhiteSpace(string Url)
            {
                if (!string.IsNullOrWhiteSpace(Url)) 
                {
                    return true;
                }
                return false;
            }

            static void NavigateTo(string Url)
            {
                if (IsNullOrWhiteSpace(Url)) // Checks if the variable Url from the constructor is NullOrWhiteSpace.
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(Url) // Opens the default web browser and navigates to specified URL from variable "Url".
                        {
                            UseShellExecute = true, // Must be set to true to be able to open default web browser.
                        });

                    }
                    catch (Exception CanNotNavigateToUri)
                    {
                        Console.WriteLine(CanNotNavigateToUri.Message);
                    }
                }

            }
        }
    }
}
