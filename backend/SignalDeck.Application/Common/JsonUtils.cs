using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SignalDeck.Application.Common
{
    public class JsonUtils
    {
        public static string? Serialize(Dictionary<string, object>? dict)
            => dict is null ? null : JsonSerializer.Serialize(dict);

        public static Dictionary<string, object>? Deserialize(string? json)
            => string.IsNullOrWhiteSpace(json)
                ? null
                : JsonSerializer.Deserialize<Dictionary<string, object>>(json);
    }
}