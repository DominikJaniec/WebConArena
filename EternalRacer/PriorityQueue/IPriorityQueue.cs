using System.Collections.Generic;

namespace EternalRacer.PriorityQueue
{
    public interface IPriorityQueue<TKey, TItem> where TItem : IPriorityItem<TKey, TItem>
    {
        bool Insert(TItem item);

        TItem PullHighest();
        TItem PeekHighest();

        void Clear();

        int Count { get; }
        int MaxElements { get; }

        bool IsFull { get; }
        bool IsEmpty { get; }
    }
}
