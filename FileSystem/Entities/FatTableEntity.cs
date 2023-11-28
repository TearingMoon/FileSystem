namespace FileSystem.Entities
{
    class FatTableEntity
    {
        public string Route;
        public bool IsDirectory;
        public int ClusterAllocation;

        public FatTableEntity(string route, bool isDirectory, int clusterAllocation)
        {
            Route = route;
            IsDirectory = isDirectory;
            ClusterAllocation = clusterAllocation;
        }
    }

}