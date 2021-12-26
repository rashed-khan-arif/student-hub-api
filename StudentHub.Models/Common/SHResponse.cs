using System;
using System.Collections.Generic; 
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Common
{
    public class SHResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
