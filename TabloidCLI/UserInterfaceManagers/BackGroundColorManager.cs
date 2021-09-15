using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BackGroundColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BackGroundColorRepository _BackGroundColorRepository;
        private string _connectionString;

    public BackGroundColorManager(IUserInterfaceManager parentUI, string connectionString)
    {
        _parentUI = parentUI;
        _BackGroundColorRepository = new BackGroundColorRepository(connectionString);
        _connectionString = connectionString;
    }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Choose background color");
            Console.WriteLine(" 1) Black");
            Console.WriteLine(" 2) Blue");
            Console.WriteLine(" 3) White");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        //{
        //    static string ChangeColor()
        //    {
        //        Console.WriteLine("Choose a Background Color");

        //        List<string> options = new List<string>()
        //                {
        //                    "black",
        //                    "blue",
        //                    "white",
        //                    "Exit"

        //                };

        //        for (int i = 0; i < options.Count; i++)
        //        {
        //            Console.WriteLine($"{i + 1}. {options[i]}");
        //        }

        //        while (true)
        //        {
        //            try
        //            {
        //                Console.WriteLine();
        //                Console.Write("Select an option > ");

        //                string input = Console.ReadLine();
        //                int index = int.Parse(input) - 1;
        //                return options[index];
        //            }
        //            catch (Exception)
        //            {

        //                continue;
        //            }
        //        }
        //    }

        //    string selection = ChangeColor();

        //    switch (selection)
        //    {
        //        case ("black"):
        //            Console.BackgroundColor = ConsoleColor.Black;
        //            Console.Clear();
        //            Console.ForegroundColor = ConsoleColor.White;
        //            break;
        //        case ("blue"):
        //            Console.BackgroundColor = ConsoleColor.Blue;
        //            Console.Clear();
        //            Console.ForegroundColor = ConsoleColor.White;
        //            break;
        //        case ("white"):
        //            Console.BackgroundColor = ConsoleColor.White;
        //            Console.Clear();
        //            Console.ForegroundColor = ConsoleColor.Black;
        //            break;
        //        case ("Exit"):
        //            return _parentUI;
        //    }
        //}
    }
}
