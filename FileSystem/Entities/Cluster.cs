using System.Drawing;

namespace FileSystem.Entities
{
    public class Cluster
    {
        public string? Name;

        public Cluster(string name)
        {
            Name = name;
        }

        public Cluster(){
            Name = null;
        }
    }

}