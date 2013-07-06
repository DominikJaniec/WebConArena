using EternalRacer.PriorityQueue;

namespace EternalRacer.Graph.Nodes
{
    public class Path : ANode, IPriorityItem<double, Path>
    {
        public double MovmentCost { get; set; }

        public double F { get { return G + H; } }
        public double G { get; private set; }
        public double H { get; set; }

        private IVertex currentAncestor;
        public override IVertex Ancestor
        {
            get { return currentAncestor; }
            set
            {
                if (currentAncestor != value)
                {
                    currentAncestor = value;
                    G = (value != null) ? currentAncestor.Pathing.G + MovmentCost : 0.0;
                }
            }
        }

        public Path(IVertex owner, double movmentCost = 1.0)
            : base(owner)
        {
            MovmentCost = movmentCost;
        }

        public override void Clear()
        {
            base.Clear();
            H = 0.0;
            G = 0.0;
        }


        public double PriorityKey { get { return F; } }
        public bool IsMoreImportantThan(Path thatOne)
        {
            return F < thatOne.F;
        }
    }
}
