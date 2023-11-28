namespace FileSystem.Data
{
    using FileSystem.Entities;

    public static class Data
    {
        //Number of clusters
        public static int clusterAmount = 16;

        //Cluster list
        public static List<Cluster> clusterList = new List<Cluster>(Config.ClusterAmmount);

        //Metadata list
        public static List<Metadata> metadataList = new List<Metadata>(Config.ClusterAmmount);

        //FatEntity list
        public static List<FatTableEntity> entityList = new List<FatTableEntity>(Config.ClusterAmmount);


        public static void dataInit()
        {
            for (int i = 0; i < Config.ClusterAmmount; i++)
            {
                Cluster newCluster = new Cluster();
                clusterList.Add(newCluster);
            }

            for (int i = 0; i < Config.ClusterAmmount; i++)
            {
                Metadata newMetadata = new Metadata();
                metadataList.Add(newMetadata);
            }

        }


    }



}
