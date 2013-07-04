using System;
using System.Diagnostics;

namespace EternalRacer.PriorityQueue
{
    [DebuggerDisplay("({PriorityKey}) {Message}")]
    public class PriorityMessageItem : IPriorityItem<int, PriorityMessageItem>
    {
        public string Message { get; private set; }

        public int PriorityKey { get; set; }

        public PriorityMessageItem(string message, int priority = 0)
        {
            PriorityKey = priority;
            Message = message;
        }

        public bool IsMoreImportantThan(PriorityMessageItem thatOne)
        {
            return PriorityKey < thatOne.PriorityKey;
        }

        public override string ToString()
        {
            return String.Format("({0}) \"{1}\"", PriorityKey, Message);
        }
    }
}
