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

bool mainmenuIterator = true;
MenuOption[] mainMenuOptions = { new MenuOption("Start", ColorEnum.Success), new MenuOption("Options (In Development)", ColorEnum.Options), new MenuOption("Exit", ColorEnum.Error) };
while (mainmenuIterator)
{
    switch (Menu.Run(header, mainMenuOptions))
    {
        case 0:
            Data.DataInit();
            bool controllerIterator = true;
            MenuOption[] controllerOptions = {
                new MenuOption("Create", ColorEnum.Success),
                new MenuOption("Create Directory", ColorEnum.Success),
                new MenuOption("Move", ColorEnum.Options),
                new MenuOption("Delete", ColorEnum.Danger),
                new MenuOption("Delete Directory", ColorEnum.Danger),
                new MenuOption("Show Scheme", ColorEnum.Important),
                new MenuOption("Exit", ColorEnum.Error) };
            while (controllerIterator)
            {
                switch (Menu.Run(header, controllerOptions))
                {
                    case 0: //Create File
                        Console.Clear();
                        FileSystemController.CreateFile();
                        Thread.Sleep(1000);
                        break;
                    case 1: // Create Directory
                        Console.Clear();
                        FileSystemController.CreateDirectory();
                        Thread.Sleep(1000);
                        break;
                    case 2: // Move File
                        break;

                    case 3: // Delete File
                        break;

                    case 4: // Delete Directory
                        break;

                    case 5: // Show Scheme
                        FileSystemController.ShowScheme();
                        break;

                    case 6: //Exit
                    default:
                        controllerIterator = false;
                        break;
                }
            }
            break;
        case 2:
            mainmenuIterator = false;
            break;
        default:
            mainmenuIterator = false;
            break;
    }
}
