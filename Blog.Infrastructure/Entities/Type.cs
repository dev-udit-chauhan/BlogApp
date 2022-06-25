using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Type
    {
        Domestic,
        International,
        City
    }
}
