using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalDeck.Application.DTOs.Application
{
    public class CreateApplicationRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}