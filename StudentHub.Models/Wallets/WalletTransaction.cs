using StudentHub.Models.Auth;
using StudentHub.Models.Enum;

namespace StudentHub.Models.Wallets
{
    public class WalletTransaction
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal TrxAmount { get; set; }
        public TrxType TrxType { get; set; }
        public DateTime TrxDate { get; set; }
        public int FromUserId { get; set; }
        public string Notes { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual User FromUser { get; set; }
    }
}
