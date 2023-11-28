namespace FileSystem.FileSystemController
{
    using FileSystem.Display;
    using FileSystem.Data;
    class FileSystemController
    {
        public static void CreateFile()
        {
            string name;
            string route;
            int size;

            name = Menu.RequestStream<string>("Type the file name and extension:");
            route = Menu.RequestStream<string>("Type the desired route:");
            size = Menu.RequestStream<int>("Type the file cluster size:");
        }
    }
}