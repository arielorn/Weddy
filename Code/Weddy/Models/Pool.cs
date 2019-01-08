using System.Collections.Generic;

namespace Weddy.Models
{
    public class Pool
    {
        public int Id { get; set; }
        public string UniqueIdentifier { get; set; }
        public string EventType { get; set; }
        public string EventName{ get; set; }
        public int Average { get; set; }

        public virtual List<Entry> Entries { get; set; }
    }
}