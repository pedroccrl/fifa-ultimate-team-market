using Newtonsoft.Json;
using System.Collections.Generic;

namespace FutApi.Models
{
    public class PlayersContent
    {
        [JsonProperty("LegendsPlayers")]
        public List<Player> LegendsPlayers { get; set; }

        [JsonProperty("Players")]
        public List<Player> Players { get; set; }
    }

    public class Player
    {
        [JsonProperty("f")]
        public string FirstName { get; set; }

        [JsonProperty("id")]
        public long AssetId { get; set; }

        [JsonProperty("l")]
        public string LastName { get; set; }

        [JsonProperty("r")]
        public long Rating { get; set; }

        [JsonProperty("c", NullValueHandling = NullValueHandling.Ignore)]
        public string Apelido { get; set; }
    }
}
