using Newtonsoft.Json;
using System.Collections.Generic;

namespace FutApi.Models
{
    public class ClubPlayers
    {
        [JsonProperty("itemData")]
        public List<ItemData> ItemData { get; set; }
    }
}
