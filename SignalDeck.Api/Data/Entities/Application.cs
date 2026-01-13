using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Api.Data.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<SignalEntity> Signals { get; set; } = new List<SignalEntity>();
    }
}