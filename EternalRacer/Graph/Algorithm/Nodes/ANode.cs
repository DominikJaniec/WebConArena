namespace EternalRacer.Graph.Algorithm.Nodes
{
    public abstract class ANode<TVertexId>
    {
        public AAlgorithmicVertex<TVertexId> Owner { get; private set; }
        public virtual AAlgorithmicVertex<TVertexId> Ancestor { get; set; }

        public virtual void Clear()
        {
            Ancestor = null;
        }

        public ANode(AAlgorithmicVertex<TVertexId> owner)
        {
            Owner = owner;
        }

        public override string ToString()
        {
            return Owner.Id.ToString();
        }
    }
}
