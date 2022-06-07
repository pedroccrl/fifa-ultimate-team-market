using Newtonsoft.Json;
using System.Collections.Generic;

namespace FutApi.Models
{
    public class ClubPlayers
    {
        [JsonProperty("itemData")]
        public List<ItemDatum> ItemData { get; set; }
    }

    public partial class ItemDatum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("formation")]
        public string Formation { get; set; }

        [JsonProperty("untradeable")]
        public bool Untradeable { get; set; }

        [JsonProperty("assetId")]
        public long AssetId { get; set; }

        [JsonProperty("rating")]
        public long Rating { get; set; }

        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        [JsonProperty("resourceId")]
        public long ResourceId { get; set; }

        [JsonProperty("owners")]
        public long Owners { get; set; }

        [JsonProperty("discardValue")]
        public long DiscardValue { get; set; }

        [JsonProperty("itemState")]
        public string ItemState { get; set; }

        [JsonProperty("cardsubtypeid")]
        public long Cardsubtypeid { get; set; }

        [JsonProperty("lastSalePrice")]
        public long LastSalePrice { get; set; }

        [JsonProperty("injuryGames")]
        public long InjuryGames { get; set; }

        [JsonProperty("preferredPosition")]
        public string PreferredPosition { get; set; }

        [JsonProperty("contract")]
        public long Contract { get; set; }

        [JsonProperty("teamid")]
        public long Teamid { get; set; }

        [JsonProperty("rareflag")]
        public long Rareflag { get; set; }

        [JsonProperty("playStyle")]
        public long PlayStyle { get; set; }

        [JsonProperty("leagueId")]
        public long LeagueId { get; set; }

        [JsonProperty("assists")]
        public long Assists { get; set; }

        [JsonProperty("lifetimeAssists")]
        public long LifetimeAssists { get; set; }

        [JsonProperty("loans", NullValueHandling = NullValueHandling.Ignore)]
        public long? Loans { get; set; }

        [JsonProperty("loyaltyBonus")]
        public long LoyaltyBonus { get; set; }

        [JsonProperty("pile")]
        public long Pile { get; set; }

        [JsonProperty("nation")]
        public long Nation { get; set; }

        [JsonProperty("marketDataMinPrice")]
        public long MarketDataMinPrice { get; set; }

        [JsonProperty("marketDataMaxPrice")]
        public long MarketDataMaxPrice { get; set; }

        [JsonProperty("resourceGameYear")]
        public long ResourceGameYear { get; set; }

        [JsonProperty("groups")]
        public List<long> Groups { get; set; }

        [JsonProperty("attributeArray")]
        public List<long> AttributeArray { get; set; }

        [JsonProperty("statsArray")]
        public List<long> StatsArray { get; set; }

        [JsonProperty("lifetimeStatsArray")]
        public List<long> LifetimeStatsArray { get; set; }

        [JsonProperty("skillmoves")]
        public long Skillmoves { get; set; }

        [JsonProperty("weakfootabilitytypecode")]
        public long Weakfootabilitytypecode { get; set; }

        [JsonProperty("attackingworkrate")]
        public long Attackingworkrate { get; set; }

        [JsonProperty("defensiveworkrate")]
        public long Defensiveworkrate { get; set; }

        [JsonProperty("preferredfoot")]
        public long Preferredfoot { get; set; }
    }
}
