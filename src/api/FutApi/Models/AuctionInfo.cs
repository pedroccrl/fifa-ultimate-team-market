using Newtonsoft.Json;

namespace FutApi.Models
{
    public partial class AuctionInfo
    {
        [JsonProperty("tradeId")]
        public long TradeId { get; set; }

        [JsonProperty("itemData")]
        public ItemData ItemData { get; set; }

        [JsonProperty("tradeState")]
        public string TradeState { get; set; }

        [JsonProperty("buyNowPrice")]
        public long BuyNowPrice { get; set; }

        [JsonProperty("currentBid")]
        public long CurrentBid { get; set; }

        [JsonProperty("offers")]
        public long Offers { get; set; }

        [JsonProperty("watched")]
        public bool Watched { get; set; }

        [JsonProperty("bidState")]
        public string BidState { get; set; }

        [JsonProperty("startingBid")]
        public long StartingBid { get; set; }

        [JsonProperty("confidenceValue")]
        public long ConfidenceValue { get; set; }

        [JsonProperty("expires")]
        public long Expires { get; set; }

        [JsonProperty("sellerName")]
        public string SellerName { get; set; }

        [JsonProperty("sellerEstablished")]
        public long SellerEstablished { get; set; }

        [JsonProperty("sellerId")]
        public long SellerId { get; set; }

        [JsonProperty("tradeOwner")]
        public bool? TradeOwner { get; set; }

        [JsonProperty("tradeIdStr")]
        public string TradeIdStr { get; set; }
    }
}
