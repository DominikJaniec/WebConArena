namespace EternalRacer.PriorityQueue
{
    public interface IPriorityQueue<T>
    {
        bool Insert(T item);
        T PullHighest();
        T PeekHighest();

        void Clear();

        int Count { get; }
        int MaxElements { get; }

        bool IsFull { get; }
        bool IsEmpty { get; }
    }
}
