using System.Diagnostics;

namespace EternalRacer.PriorityQueue
{
    [DebuggerDisplay("({Priority}) {Message}")]
    public class MessageItem : IPriorityItem
    {
        public int Priority { get; private set; }
        public string Message { get; private set; }

        public MessageItem(string message, int priority = 0)
        {
            Priority = priority;
            Message = message;
        }

        public bool IsMoreImportantThan(IPriorityItem thatOne)
        {
            return this.Priority < thatOne.Priority;
        }
    }
}
