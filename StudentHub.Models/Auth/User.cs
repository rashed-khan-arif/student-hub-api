using Microsoft.AspNetCore.Identity;
using StudentHub.Models.AssetExchanges;
using StudentHub.Models.Payment;
using StudentHub.Models.Purchase;
using StudentHub.Models.Students;
using StudentHub.Models.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace StudentHub.Models.Auth
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Sex { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public virtual Student Student { get; set; }
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual ICollection<AssetPurchase> AssetPurchases { get; set; }
        public virtual ICollection<AssetPayment> AssetPayments { get; set; }
        public virtual ICollection<StudentAsset> StudentAssets { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual ICollection<WalletTransaction> Transactions { get; set; }
    }
}
