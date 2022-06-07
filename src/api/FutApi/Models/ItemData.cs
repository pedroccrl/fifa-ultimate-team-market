using System;
using System.Collections.Generic;

namespace FutApi.Models
{
    public partial class ItemData
    {
        public long Id { get; set; }
        public long Timestamp { get; set; }
        public string Formation { get; set; }
        public bool Untradeable { get; set; }
        public long AssetId { get; set; }
        public long Rating { get; set; }
        public string ItemType { get; set; }
        public long ResourceId { get; set; }
        public long Owners { get; set; }
        public long DiscardValue { get; set; }
        public string ItemState { get; set; }
        public long Cardsubtypeid { get; set; }
        public long LastSalePrice { get; set; }
        public string InjuryType { get; set; }
        public long? InjuryGames { get; set; }
        public string PreferredPosition { get; set; }
        public long? Contract { get; set; }
        public long Teamid { get; set; }
        public long Rareflag { get; set; }
        public long? PlayStyle { get; set; }
        public long LeagueId { get; set; }
        public long? Assists { get; set; }
        public long? LifetimeAssists { get; set; }
        public long? LoyaltyBonus { get; set; }
        public long Pile { get; set; }
        public long? Nation { get; set; }
        public long MarketDataMinPrice { get; set; }
        public long MarketDataMaxPrice { get; set; }
        public long ResourceGameYear { get; set; }
        public List<long> Groups { get; set; }
        public List<long> AttributeArray { get; set; }
        public List<long> StatsArray { get; set; }
        public List<long> LifetimeStatsArray { get; set; }
        public long? Skillmoves { get; set; }
        public long? Weakfootabilitytypecode { get; set; }
        public long? Attackingworkrate { get; set; }
        public long? Defensiveworkrate { get; set; }
        public long? Preferredfoot { get; set; }
        public List<object> StatsList { get; set; }
        public List<object> LifetimeStats { get; set; }
        public List<object> AttributeList { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? Negotiation { get; set; }
        public Guid? GuidAssetId { get; set; }
        public long? Cardassetid { get; set; }
        public long? Value { get; set; }
        public long? Category { get; set; }
        public string Name { get; set; }
        public long? Weightrare { get; set; }
        public string Description { get; set; }
        public string Header { get; set; }
        public string Biodescription { get; set; }
        public long? ChantsCount { get; set; }
        public bool? Authenticity { get; set; }
        public long? ShowCasePriority { get; set; }
    }
}
