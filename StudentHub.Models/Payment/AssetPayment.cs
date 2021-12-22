using StudentHub.Models.Auth;
using StudentHub.Models.Enum;
using StudentHub.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Payment
{
    public class AssetPayment
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaymentBy { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public virtual AssetPurchase AssetPurchase { get; set; }
        public virtual User PaymentUser { get; set; }

    }
}
