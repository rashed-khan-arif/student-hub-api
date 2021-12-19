using StudentHub.Models.Auth;
using StudentHub.Models.Enum;
using StudentHub.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.AssetExchanges
{
    public class StudentAsset
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public AssetStatus Status { get; set; }
        public AssetType Type { get; set; }
        public AssetCategory Category { get; set; }
        public bool IsUsed { get; set; }        
        //Ex. 36 days
        public int UsedDuration { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
