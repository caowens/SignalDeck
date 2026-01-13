using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalDeck.Domain.Common;

namespace SignalDeck.Domain.Entities
{
    public class Application
    {
        public int InternalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<SignalEntity> Signals { get; set; } = new List<SignalEntity>();
    }
}