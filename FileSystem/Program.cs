// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using FileSystem.Display;
using FileSystem.Entities;
using FileSystem.Data;
using FileSystem.FileSystemController;

Console.ForegroundColor = ConsoleColor.Yellow;

bool mainmenuIterator = true;
MenuOption[] mainMenuOptions = { new MenuOption("Start", ColorEnum.Success), new MenuOption("Options (In Development)", ColorEnum.Options), new MenuOption("Exit", ColorEnum.Error) };
while (mainmenuIterator)
{
    switch (Menu.Run(Config.header, mainMenuOptions))
    {
        case 0:
            Data.DataInit();
            bool controllerIterator = true;
            MenuOption[] controllerOptions = {
                new MenuOption("Create", ColorEnum.Success),
                new MenuOption("Create Directory", ColorEnum.Success),
                new MenuOption("Move", ColorEnum.Options),
                new MenuOption("Move Directory", ColorEnum.Options),
                new MenuOption("Delete File", ColorEnum.Danger),
                new MenuOption("Delete Directory", ColorEnum.Danger),
                new MenuOption("Show Scheme", ColorEnum.Important),
                new MenuOption("Show Dir Content", ColorEnum.Important),
                new MenuOption("Exit", ColorEnum.Error) };
            while (controllerIterator)
            {
                switch (Menu.Run(Config.header, controllerOptions))
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
                        Console.Clear();
                        FileSystemController.MoveFile();
                        Thread.Sleep(1000);
                        break;

                    case 3: //Move Directory
                        Console.Clear();
                        FileSystemController.MoveDirectory();
                        Thread.Sleep(1000);
                        break;

                    case 4: // Delete File
                        Console.Clear();
                        FileSystemController.DeleteFile();
                        Thread.Sleep(1000);
                        break;

                    case 5: // Delete Directory
                        Console.Clear();
                        FileSystemController.DeleteDirectory();
                        Thread.Sleep(1000);
                        break;

                    case 6: // Show Scheme
                        FileSystemController.ShowScheme();
                        break;

                    case 7: //Show Dir Content
                        Console.Clear();
                        FileSystemController.ShowDirContent();
                        break;

                    case 8: //Exit
                    default:
                        controllerIterator = false;
                        break;
                }
            }
            break;
        case 1:
            Config.ConfigMenu();
            break;
        case 2:
        default:
            mainmenuIterator = false;
            break;
    }
}
