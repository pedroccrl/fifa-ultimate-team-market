using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FutApi.Models
{
    public class TransferList
    {
        [JsonProperty("auctionInfo")]
        public List<AuctionInfo> AuctionInfo { get; set; }
    }
}
