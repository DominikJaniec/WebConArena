using EternalRacer.PriorityQueue;

namespace EternalRacer.Graph
{
    public class NodePathing : IPriorityItem<double, NodePathing>
    {
        public double MovmentCost { get; private set; }

        public IVertex Current { get; private set; }

        public double F { get; private set; }
        public double G { get; private set; }
        public double H { get; private set; }


        IVertex currentAncestor;
        public IVertex Ancestor
        {
            get { return currentAncestor; }
            set
            {
                if (currentAncestor != value)
                {
                    currentAncestor = value;
                    G = currentAncestor.PathingNode.G + MovmentCost;
                }
            }
        }

        public NodePathing(IVertex current, double movmentCost = 1.0)
        {
            MovmentCost = movmentCost;
            Current = current;
            ClearAndSet(0.0);
        }

        public void ClearAndSet(double heuristic)
        {
            currentAncestor = null;
            H = 0.0;
            G = 0.0;
        }


        public double PriorityKey { get { return F; } }
        public bool IsMoreImportantThan(NodePathing thatOne)
        {
            return F < thatOne.F;
        }
    }
}
