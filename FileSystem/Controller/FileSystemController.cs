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
                Data.metadataList[clusterId].NextCluster = -1;
                Data.clusterList[clusterId].Name = name;
            }
        }

        public static void MoveFile()
        {
            string oldPath = IsValidOldPath();
            string newPath = IsValidNewPath(oldPath);

            var oldEntity = Data.entityList.FirstOrDefault(x => x.Path.ToLowerInvariant().Equals(oldPath.ToLowerInvariant()) && !x.IsDirectory);
            if (oldEntity != null)
            {
                Data.entityList[Data.entityList.IndexOf(oldEntity)].Path = newPath;
            }
        }

        public static void DeleteFile(string oldPath = "")
        {
            if (oldPath == "")
            {
                oldPath = IsValidOldPath();
            }
            var oldEntity = Data.entityList.FirstOrDefault(x => x.Path.ToLowerInvariant().Equals(oldPath.ToLowerInvariant()) && !x.IsDirectory);
            if (oldEntity != null)
            {
                Data.entityList.Remove(oldEntity);

                int metadataPosition = oldEntity.ClusterAllocation;

                bool Continue = true;

                while (Continue)
                {
                    Data.metadataList[metadataPosition].Avaliable = true;

                    if (Data.metadataList[metadataPosition].End)
                    {
                        Continue = false;
                    }
                    if (Data.metadataList[metadataPosition].NextCluster != -1)
                    {
                        metadataPosition = Data.metadataList[metadataPosition].NextCluster;
                    }
                }
            }
        }

        public static void DeleteDirectory()
        {
            string route = IsValidRoute();
            
            /*
            List<FatTableEntity> coincidences = Data.entityList.Where(x => x.Path.ToLowerInvariant().Contains(route.ToLowerInvariant()) && !x.IsDirectory).ToList();
            foreach (var item in coincidences)
            {
                DeleteFile(item.Path);
            }
            */

            //NUEVO: Es el mismo bucle que el de los archivos solo que buscando directorios tambien, y borra el directorio/archivo
            //dejo los otros dos comentados para no borrarlos, por si hay que cambiar algo

            List<FatTableEntity> coincidences = Data.entityList.Where(x => x.Path.ToLowerInvariant().Contains(route.ToLowerInvariant())).ToList();
            foreach (var item in coincidences)
            {
                if(item.IsDirectory)
                {
                    Data.metadataList[item.ClusterAllocation].Avaliable = true;
                    Data.metadataList[item.ClusterAllocation].End = false;
                    Data.metadataList[item.ClusterAllocation].NextCluster = -1;
                    Data.entityList.Remove(item);
                }
                else
                {
                    DeleteFile(item.Path);
                }
                
            }

            /*
            var result = Data.entityList.FirstOrDefault(x => x.Path.ToLowerInvariant().Equals(route.ToLowerInvariant()) && x.IsDirectory);
            if (result != null)
            {
                Data.metadataList[result.ClusterAllocation].Avaliable = true;
                Data.metadataList[result.ClusterAllocation].End = false;
                Data.metadataList[result.ClusterAllocation].NextCluster = -1;
                Data.entityList.Remove(result);
            }
            */
        }

        public static void MoveDirectory(){
            string oldPath = IsValidOldPathDirectory();
            string newPath = IsValidNewPathDirectory(oldPath) + '/';

            var oldEntity = Data.entityList.FirstOrDefault(x => x.Path.ToLowerInvariant().Equals(oldPath.ToLowerInvariant()) && x.IsDirectory);


            if (oldEntity != null)
            {
                List<FatTableEntity> results = Data.entityList.Where(x => x.Path.ToLowerInvariant().Contains(oldEntity.Path.ToLowerInvariant())).ToList();
                foreach (var item in results)
                {
                    var index = Data.entityList.IndexOf(item);
                    Data.entityList[index].Path = Data.entityList[index].Path.ToLowerInvariant().Replace(oldPath.ToLowerInvariant(), newPath);
                }
                Data.entityList[Data.entityList.IndexOf(oldEntity)].Path = newPath;
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
                Console.Write($"{(Data.metadataList[i].NextCluster != -1 ? Data.metadataList[i].NextCluster : "Empty"),-11}");
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

        #region Private Methods
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

                //NUEVO: He añadido la ultima condicion para que no se pueda dejar en blanco la extension del archivo
                if (input != null && input.Trim() != "" && stringParts.Length >= 2 && stringParts[(stringParts.Length)-1] != "")
                {
                    if (!Data.FileExists(route + input))
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

        private static string IsValidNewPath(string oldPath)
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

                var oldPathSplits = oldPath.Split('/');

                if (input != null && input.Trim() != "" && coincidence != null)
                {
                    var fullPath = input + oldPathSplits[oldPathSplits.Length - 1];
                    if (!Data.FileExists(fullPath))
                    {
                        return fullPath;
                    }
                    else
                    {
                        Menu.Write("Theres a file with the same name on that path", ColorEnum.ErrorNoBg);

                    }
                }
                else
                {
                    Menu.Write("Incorrect path, you must input a valid path", ColorEnum.ErrorNoBg);
                }
            }
        }

        private static string IsValidOldPath()
        {
            while (true)
            {
                Console.WriteLine("");
                var input = Menu.RequestStream<string>("Type the old file path:");
                if (Data.FileExists(input))
                {
                    return input;
                }
                else
                {
                    Menu.Write("That file doesn't exist", ColorEnum.ErrorNoBg);
                }
            }
        }

        private static string IsValidOldPathDirectory(){
            while (true)
            {
                Console.WriteLine("");
                var input = Menu.RequestStream<string>("Type the old directory path:");
                if (Data.DirectoryExists(input))
                {
                    return input;
                }
                else
                {
                    Menu.Write("That directory doesn't exist", ColorEnum.ErrorNoBg);
                }
            }
        }

        private static string IsValidNewPathDirectory(string oldPath)
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

                var oldPathSplits = oldPath.Split('/');

                if (input != null && input.Trim() != "" && coincidence != null)
                {
                    var fullPath = input + oldPathSplits[oldPathSplits.Length - 2];
                    if (!Data.DirectoryExists(fullPath))
                    {
                        return fullPath;
                    }
                    else
                    {
                        Menu.Write("Theres a directory with the same name on that path", ColorEnum.ErrorNoBg);
                    }
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

                if(input<=0)
                {
                    Menu.Write("File size is equal or less than 0, introduce a valid size", ColorEnum.ErrorNoBg);
                }
                else
                {
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

        #endregion
    }
}