// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using FileSystem.Display;
using FileSystem.Entities;
using FileSystem.Data;
using FileSystem.FileSystemController;

Console.ForegroundColor = ConsoleColor.Yellow;
string header = @"
______ _ _        _____           _                 
|  ___(_) |      /  ___|         | |                
| |_   _| | ___  \ `--. _   _ ___| |_ ___ _ __ ___  
|  _| | | |/ _ \  `--. \ | | / __| __/ _ \ '_ ` _ \ 
| |   | | |  __/ /\__/ / |_| \__ \ ||  __/ | | | | |
\_|   |_|_|\___| \____/ \__, |___/\__\___|_| |_| |_|
                         __/ |                      
                        |___/                        
-----------------------------------------------------------------------------------------
by Carlos Vicente, David Torrubia, Eduardo Villar, Alvaro García and Adrián Liso
-----------------------------------------------------------------------------------------";

MenuOption[] mainMenuOptions = { new MenuOption("Prueba"), new MenuOption("Verde", ColorEnum.Success)};
int index = Menu.Run(header, mainMenuOptions);
Console.WriteLine($"You selected:{mainMenuOptions[index].Text}");

//LLenar listas clusters y metadatos
Data.dataInit();
FileSystemController.CreateFile();
