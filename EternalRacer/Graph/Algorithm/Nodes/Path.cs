using EternalRacer.PriorityQueue;

namespace EternalRacer.Graph.Algorithm.Nodes
{
    public class Path<TVertexId> : ANode<TVertexId>, IPriorityItem<double, Path<TVertexId>>
    {
        public double F { get { return G + H; } }
        public double G { get; private set; }
        public double H { get; private set; }

        private AAlgorithmicVertex<TVertexId> currentAncestor;
        public override AAlgorithmicVertex<TVertexId> Ancestor
        {
            get { return currentAncestor; }
            set
            {
                if (currentAncestor != value)
                {
                    currentAncestor = value;
                    G = (value != null) ? currentAncestor.Pathing.G + currentAncestor.MovementCost(Owner) : 0.0;
                }
            }
        }

        public Path(AAlgorithmicVertex<TVertexId> owner)
            : base(owner) { }

        public override void Clear()
        {
            base.Clear();

            H = 0;
            G = 0;
        }

        public void HRecalculate(AAlgorithmicVertex<TVertexId> goal)
        {
            H = Owner.DistanceTo(goal);
        }


        public double PriorityKey { get { return F; } }
        public bool IsMoreImportantThan(Path<TVertexId> thatOne)
        {
            return F < thatOne.F;
        }
    }
}
