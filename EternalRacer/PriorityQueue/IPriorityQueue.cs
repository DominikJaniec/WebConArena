using System.Collections.Generic;

namespace EternalRacer.PriorityQueue
{
    public interface IPriorityQueue<TKey, TItem> : ICollection<TItem> where TItem : IPriorityItem<TKey, TItem>
    {
        bool ItemPriorityChanged(TItem item);
        bool Insert(TItem item);

        TItem PullHighest();
        TItem PeekHighest();        

        int Size { get; }

        bool IsFull { get; }
        bool IsEmpty { get; }
    }
}
