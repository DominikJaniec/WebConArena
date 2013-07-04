using System.Diagnostics;

namespace EternalRacer.PriorityQueue
{
    [DebuggerDisplay("({PriorityKey}) {Message}")]
    public class MessageItem : IPriorityItem<int, MessageItem>
    {
        public string Message { get; private set; }

        public int PriorityKey { get; set; }

        public MessageItem(string message, int priority = 0)
        {
            PriorityKey = priority;
            Message = message;
        }

        public bool IsMoreImportantThan(MessageItem thatOne)
        {
            return PriorityKey < thatOne.PriorityKey;
        }
    }
}
