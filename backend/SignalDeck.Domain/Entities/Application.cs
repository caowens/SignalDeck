using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Common;

namespace SignalDeck.Domain.Entities
{
    public class Application : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}