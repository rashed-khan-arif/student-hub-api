using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Purchase
{
    public class AssetPurchase
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PurchasedBy { get; set; }
    }
}
