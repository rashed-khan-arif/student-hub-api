using StudentHub.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Wallets
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<WalletTransaction> Transactions { get; set; }
    }
}
