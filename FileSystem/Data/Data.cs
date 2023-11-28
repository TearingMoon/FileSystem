namespace FileSystem.Data
{
    class Data
    {
        //Number of clusters
        int clusterAmount = 16;

        //Cluster list
        List<Cluster> clusterList = new List<Cluster>(Config.ClusterAmmount);
        for(int i=0;i<Config.ClusterAmmount;i++)
        {
            Cluster newCluster = new Cluster();
            clusterList.Add(newCluster);
        }

        //Metadata list
        List<Metadata> metadataList = new List<Metadata>(Config.ClusterAmmount);
        for(int i=0;i<Config.ClusterAmmount;i++)
        {
            Metadata newMetadata = new Metadata();
            metadataList.Add(newMetadata);
        }

        //FatEntity list
        List<FatTableEntity> entityList = new List<FatTableEntity>(Config.ClusterAmmount);
    }

}