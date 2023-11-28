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


        public static void DataInit()
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

            //Creation of initial directory
            clusterList[0].Name = "C:/";
            metadataList[0].Avaliable = false;
            metadataList[0].End = true;
            metadataList[0].NextCluster = null;
            entityList.Add(new FatTableEntity("C:/", true, clusterAllocation:0));
        }

        public static int GetfirstAvaliableMetadata(){
            var stash = metadataList.FirstOrDefault(x => x.Avaliable && !x.Damaged && !x.Reserved);
            if (stash != null){
                return metadataList.IndexOf(stash);
            } else {
                return -1;
            }
        }
    }



}
