namespace FileSystem.FileSystemController
{
    using FileSystem.Display;
    using FileSystem.Data;
    using FileSystem.Entities;

    class FileSystemController
    {
        public static void CreateFile()
        {
            string name;
            string route;
            int size;

            name = IsValidFileName();
            route = IsValidRoute();
            size = WillFileFit();
        }

        private static string IsValidFileName()
        {
            while (true)
            {
                Console.WriteLine("");
                var input = Menu.RequestStream<string>("Type the file name and extension:");
                string[] stringParts = input.Split('.');
                if (input != null && input.Trim() != "" && stringParts.Length >= 2)
                {
                    return input;
                }
                else
                {
                    Menu.Write("Incorrect file name or extension, you must input a name and extension", ColorEnum.ErrorNoBg);
                }
            }
        }

        private static string IsValidRoute()
        {
            while (true)
            {
                Console.WriteLine("");
                var input = Menu.RequestStream<string>("Type the desired path:");
                FatTableEntity? coincidence;

                if (input != null)
                {
                    coincidence = Data.entityList.FirstOrDefault(x => x.Path.ToLowerInvariant().Equals(input.ToLowerInvariant()) && x.IsDirectory);
                }
                else
                {
                    coincidence = null;
                }

                if (input != null && input.Trim() != "" && coincidence != null)
                {
                    return input;
                }
                else
                {
                    Menu.Write("Incorrect path, you must input a valid path", ColorEnum.ErrorNoBg);
                }
            }
        }

        private static int WillFileFit()
        {
            while (true)
            {
                Console.WriteLine("");
                var input = Menu.RequestStream<int>("Type the file cluster size:");

                List<Metadata> AvaliableCluster = Data.metadataList.Where(x => x.Avaliable && !x.Reserved && !x.Damaged).ToList();

                if (AvaliableCluster.Count >= input)
                {
                    return input;
                }
                else
                {
                    Menu.Write("The File is too heavy to fit, change file size", ColorEnum.ErrorNoBg);
                }
            }
        }
    }
}