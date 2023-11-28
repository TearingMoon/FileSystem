using System.Runtime.CompilerServices;

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

            route = IsValidRoute();
            name = IsValidFileName(route);
            size = WillFileFit();

            for (int i = 0; i < size; i++)
            {
                int clusterId = Data.GetfirstAvaliableMetadata();
                if (i == 0)
                {
                    Data.entityList.Add(new FatTableEntity(route + name, false, clusterId));
                }

                Data.metadataList[clusterId].Avaliable = false;
                if (i != size - 1)
                {
                    Data.metadataList[clusterId].End = false;
                    Data.metadataList[clusterId].NextCluster = Data.GetfirstAvaliableMetadata();
                }
                else
                {
                    Data.metadataList[clusterId].End = true;
                }

                Data.clusterList[clusterId].Name = name;
            }
        }

        public static void CreateDirectory()
        {
            List<Metadata> AvaliableCluster = Data.metadataList.Where(x => x.Avaliable && !x.Reserved && !x.Damaged).ToList();
            if (AvaliableCluster.Count >= 1)
            {
                string route = IsValidRoute();
                string name = IsValidDirectoryName(route);
                name = name + '/';
                int clusterId = Data.GetfirstAvaliableMetadata();
                Data.entityList.Add(new FatTableEntity(route + name, true, clusterId));
                Data.metadataList[clusterId].Avaliable = false;
                Data.metadataList[clusterId].End = true;
                Data.clusterList[clusterId].Name = name;
            }
        }

        public static void ShowScheme()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("| METADATA                                                              |");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("| Cluster | Avaliable | Damaged | Reserved |       | Next     | End     |");
            Console.WriteLine("-------------------------------------------------------------------------");
            for (int i = 0; i < Data.metadataList.Count; i++)
            {
                Console.Write($"{"",-2}");
                Console.Write($"{i,-10}");
                Console.Write($"{Data.metadataList[i].Avaliable,-12}");
                Console.Write($"{Data.metadataList[i].Damaged,-10}");
                Console.Write($"{Data.metadataList[i].Reserved,-12}");
                Console.Write("       ");
                Console.Write($"{(Data.metadataList[i].NextCluster != null ? Data.metadataList[i].NextCluster : "Empty"),-11}");
                Console.Write($"{Data.metadataList[i].End,-5}");
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Press any key to continue =>");
            Console.ReadLine();

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("| Fat Table                                                             |");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("| Route                                           | Type      | Cluster |");
            Console.WriteLine("-------------------------------------------------------------------------");
            for (int i = 0; i < Data.entityList.Count; i++)
            {
                Console.Write($"{"",-2}");
                Console.Write($"{Data.entityList[i].Path,-50}");
                Console.Write($"{(Data.entityList[i].IsDirectory ? "Directory" : "File"),-12}");
                Console.Write($"{Data.entityList[i].ClusterAllocation,-10}");
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Press any key to continue =>");
            Console.ReadLine();

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("| Clusters                                                              |");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("| Cluster | FileName                                                    |");
            Console.WriteLine("-------------------------------------------------------------------------");
            for (int i = 0; i < Data.clusterList.Count; i++)
            {
                Console.Write($"{"",-2}");
                Console.Write($"{i,-11}");
                Console.Write($"{(Data.clusterList[i].Name != "" && Data.clusterList[i].Name != null ? Data.clusterList[i].Name : "~ EMPTY ~"),-11}");
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Press any key to continue =>");
            Console.ReadLine();
        }

        private static string IsValidDirectoryName(string route)
        {
            while (true)
            {
                Console.WriteLine("");
                var input = Menu.RequestStream<string>("Type the desired name:");

                if (input != null && input.Trim() != "")
                {
                    var coincidence = Data.entityList.FirstOrDefault(x => x.Path.ToLowerInvariant().Contains((route + input).ToLowerInvariant()) && x.IsDirectory);

                    if (coincidence == null)
                    {
                        return input;
                    }
                    else
                    {
                        Menu.Write("That file already exists in that directory, choose another name", ColorEnum.ErrorNoBg);
                    }
                }
                else
                {
                    Menu.Write("Incorrect file name or extension, you must input a name and extension", ColorEnum.ErrorNoBg);
                }
            }
        }

        private static string IsValidFileName(string route)
        {
            while (true)
            {
                Console.WriteLine("");
                var input = Menu.RequestStream<string>("Type the file name and extension:");
                string[] stringParts = input.Split('.');

                if (input != null && input.Trim() != "" && stringParts.Length >= 2)
                {
                    var coincidence = Data.entityList.FirstOrDefault(x => x.Path.ToLowerInvariant().Contains((route + input).ToLowerInvariant()) && !x.IsDirectory);

                    if (coincidence == null)
                    {
                        return input;
                    }
                    else
                    {
                        Menu.Write("That file already exists in that directory, choose another name", ColorEnum.ErrorNoBg);
                    }
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