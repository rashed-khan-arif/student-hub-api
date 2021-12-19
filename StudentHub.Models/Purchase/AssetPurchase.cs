using StudentHub.Models.Auth;
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
        public int UserId { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal Vat { get; set; }
        public decimal Tax { get; set; }
        public DateTime PurchaseDate { get; set; } 
        public virtual User User { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
