using System.Collections.Generic;

namespace FutApi.Models
{
    public class PlayersTradePile
    {
        public long Credits { get; set; }
        public List<AuctionInfo> AuctionInfo { get; set; }
        public List<DuplicateItemIdList> DuplicateItemIdList { get; set; }
    }

    public partial class DuplicateItemIdList
    {
        public long ItemId { get; set; }
        public long DuplicateItemId { get; set; }
    }
}
