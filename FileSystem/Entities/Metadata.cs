using System.Threading.Tasks.Dataflow;

namespace FileSystem.Entities
{
    class Metadata
    {
        public bool Avaliable;
        public bool Damaged;
        public bool Reserved;

        public bool End;
        public int? NextCluster;
        public Metadata(bool avaliable, bool damaged, bool reserved, bool end, int nextCluster)
        {
            Avaliable = avaliable;
            Damaged = damaged;
            Reserved = reserved;
            End = end;
            NextCluster = nextCluster; 
        }

        public Metadata(){
            Avaliable = true;
            Damaged = false;
            Reserved = false;
            End = false;
            NextCluster = null;
        }
    }

}