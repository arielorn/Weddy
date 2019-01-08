using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weddy.Models;

namespace Weddy.ViewModels
{
    public class PoolViewModel
    {
        public string UniqueIdentifier { get; set; }
        public string EventType { get; set; }
        public string EventName{ get; set; }
        public double Average { get; set; }

        public virtual IEnumerable<EntryViewModel> ActiveEntries { get; set; }
    }
}