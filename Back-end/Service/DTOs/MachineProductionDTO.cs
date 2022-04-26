using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class MachineProductionDTO
    {
        [JsonPropertyName("machineId")]
        public int MachineId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("production")]
        public int TotalProduction { get; set; }
    }
}
