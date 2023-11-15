﻿// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine(@"  _____ _ _                ____            _                 ");
Console.WriteLine(@" |  ___(_) | ___          / ___| _   _ ___| |_ ___ _ __ ___  ");
Console.WriteLine(@" | |_  | | |/ _ \  _____  \___ \| | | / __| __/ _ \ '_ ` _ \ ");
Console.WriteLine(@" |  _| | | |  __/ |_____|  ___) | |_| \__ \ ||  __/ | | | | |");
Console.WriteLine(@" |_|   |_|_|\___|         |____/ \__, |___/\__\___|_| |_| |_|");
Console.WriteLine(@"                                 |___/                       ");
Console.WriteLine(@"-------------------------------------------------------------");
Console.ForegroundColor = ConsoleColor.White;

string? input = "Continue";
while (input != null && input != "exit")
{
    Console.WriteLine("Select an action:");
    Console.WriteLine(@"-------------------------------------------------------------");
    System.Console.WriteLine("1. ");
    System.Console.WriteLine("2. ");
    System.Console.WriteLine("3. ");
    System.Console.WriteLine("4. ");
    System.Console.WriteLine("5. ");
    System.Console.WriteLine("6. ");
    System.Console.WriteLine("7. ");
    System.Console.WriteLine("8. ");

    input = Console.ReadLine();

    System.Console.WriteLine("You selected: " + input);
}
