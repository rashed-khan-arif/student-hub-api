using StudentHub.Models.AssetExchanges;

namespace StudentHub.Models.Purchase
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int AssetPurchaseId { get; set; }
        public decimal SalesPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual StudentAsset Asset { get; set; }
        public virtual AssetPurchase AssetPurchase { get; set; }

    }
}
