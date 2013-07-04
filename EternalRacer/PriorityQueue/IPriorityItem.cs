namespace EternalRacer.PriorityQueue
{
    public interface IPriorityItem<TKey, TItem>
    {
        TKey PriorityKey { get; }

        bool IsMoreImportantThan(TItem thatOne);
    }
}
