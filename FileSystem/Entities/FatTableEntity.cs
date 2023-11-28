namespace FileSystem.Entities
{
    public class FatTableEntity
    {
        public string Path;
        public bool IsDirectory;
        public int ClusterAllocation;

        public FatTableEntity(string route, bool isDirectory, int clusterAllocation)
        {
            Path = route;
            IsDirectory = isDirectory;
            ClusterAllocation = clusterAllocation;
        }
    }

}