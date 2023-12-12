using FileSystem.Display;

public static class Config
{
    public static int ClusterAmmount = 16;
    public static bool PersistentData = false;

    public static string header = @"
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
    public static void ConfigMenu()
    {
        bool ConfigIterator = true;
        while (ConfigIterator)
        {
            MenuOption[] controllerOptions = {
                new MenuOption($"Cluster Size:{Config.ClusterAmmount}", ColorEnum.Options),
                new MenuOption($"Persistent Data (Not Implemented):{Config.PersistentData}", ColorEnum.Options),
                new MenuOption("Exit", ColorEnum.Error)
            };
            switch (Menu.Run(Config.header, controllerOptions))
            {
                case 0:
                    Console.Clear();
                    Config.ClusterAmmount = Menu.RequestStream<int>("Input new cluster size:");
                    Thread.Sleep(1000);
                    break;
                case 1:
                    Console.Clear();
                    Config.PersistentData = Menu.RequestStream<bool>("Type true or false to set persistent data");
                    break;
                case 2:
                default:
                    ConfigIterator = false;
                    break;
            }
        }
    }
}