using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.AssetExchanges
{
    public class AssetImage
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string ImageTitle { get; set; }
        public int ImageUrl { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
