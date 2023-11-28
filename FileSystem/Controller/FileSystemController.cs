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

            name = IsFileName();
            route = Menu.RequestStream<string>("Type the desired route:");
            size = Menu.RequestStream<int>("Type the file cluster size:");
        }

        private static string IsFileName (){
            while (true)
            {
                var input = Menu.RequestStream<string>("Type the file name and extension:");
                string[] stringParts = input.Split('.');
                if (input != null && input.Trim() != "" && stringParts.Length >= 2)
                {
                    return input;
                }
                else
                {
                    Menu.Write("Incorrect file name or extension, you must input a name and extension", ColorEnum.Error);
                }
            }
        }
    }
}