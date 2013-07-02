namespace EternalRacer.PriorityQueue
{
    public interface IPriorityItem
    {
        int Priority { get; }
        bool IsMoreImportantThan(IPriorityItem thatOne);
    }
}
