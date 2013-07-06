namespace EternalRacer.Graph
{
    public abstract class ANode
    {
        public IVertex Owner { get; private set; }
        public virtual IVertex Ancestor { get; set; }

        public virtual void Clear()
        {
            Ancestor = null;
        }

        public ANode(IVertex owner)
        {
            Owner = owner;
            Clear();
        }
    }
}
