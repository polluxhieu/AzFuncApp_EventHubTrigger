using System;
using System.Collections.Generic;
using System.Text;

namespace TriggersInOut
{
    public class EventResult
    {
        public DateTime EnqueuedTimeUtc { get; set; }
        public long SequenceNumber { get; set; }
        public string Offset { get; set; }
    }
}
