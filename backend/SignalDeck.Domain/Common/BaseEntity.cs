using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}