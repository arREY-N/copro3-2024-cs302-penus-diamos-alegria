﻿using System;
using System.Text.RegularExpressions;

namespace CharacterCreationSystem
{
    // Class containing input and validation methods
    public class Utility
    {
        // Method to display action and get input
        public static string GetInput(string action)
        {
            Console.Write($"{action}: ");
            return Console.ReadLine() ?? String.Empty;
        }
        // Validation method for int input
        public static int Validate(String input, int key)
        {
            try
            {
                return Convert.ToInt32(input);
            } 
            catch (Exception e) when (e is FormatException || e is OverflowException)
            {
                throw new FormatException("Input must be a valid integer number!");
            }   
        }
        // Validation method for string input
        public static string Validate(String input, char key)
        {
            string? text;

            if (Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
            {
                text = input;
            }
            else
            {
                throw new FormatException("Only alphanumeric characters are allowed in the input!");
            }
            return text ?? throw new Exception("Empty string!");
        }
        // Validation method for pirate names
        public static string ValidateName(string input)
        {
            string? name;

            if (Database.pirateDictionary.ContainsKey(input))
            {
                throw new NameUnavailableException("Name already found in the system, use another one!");
            } else if (input.Length < 4)
            {
                throw new ArgumentException("Name must be more than 4 characters long!");
            } 

            return input;
        }
        // Program buffer
        public static void EnterToContinue()
        {
            Console.WriteLine();
            Divider();
            DisplayCenter("Press ENTER to continue...");
            Console.ReadKey();
            Console.Clear();
            Loading();
        }
        // Loading screen
        public static void Loading()
        {
            Console.Clear();
            DisplayHeader("LOADING");
            Thread.Sleep(500);
            Console.Clear();
        }
        // Displaying formatted error message
        public static void DisplayErrorMessage(string message)
        {
            Console.Clear();
            DisplayHeader(message);
            EnterToContinue();
        }
        // Center formatting 
        public static void DisplayCenter(string text)
        {
            int LRmargin = (Console.WindowWidth - text.Length) / 2;
            int Wcenter = Console.WindowWidth / 2;
            string margin = new string(' ', LRmargin);
            string center = new string(' ', Wcenter);

            Console.Write($"\n{margin}{text}{margin}\n");
        }
        // Displays the game name and section title
        public static void DisplayHeader(string section)
        {
            Console.Clear();
            DisplayCenter("SeaPAG: A Sea Pirate Adventure Game");
            DisplayCenter("\u00A9 2024\n");
            Divider();
            DisplayCenter(section);
            Console.WriteLine();
            Divider();
            Console.WriteLine();
        }
        // Method to confirm action
        public static bool Confirm()
        {
            Console.WriteLine("Confirm action");
            Console.WriteLine("| 1  | Proceed");
            Console.WriteLine("| 2  | Cancel");
            while (true)
            {
                try
                {
                    int confirm = Utility.Validate(Utility.GetInput("Action"), 1);
                    switch (confirm)
                    {
                        case 1:
                            return true;
                        case 2:
                            return false;
                        default:
                            throw new OptionUnavailableException($"{confirm} is not in the option");
                    }
                } 
                catch (Exception e) when (e is OptionUnavailableException || e is FormatException)
                {
                    Utility.DisplayErrorMessage(e.Message);
                }
            }
        }
        public static void Divider()
        {
            Console.WriteLine(new string('=', Console.WindowWidth));
        }
    }
}