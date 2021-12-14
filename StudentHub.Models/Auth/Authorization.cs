using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Auth
{
    public class Authorization
    {
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
        public UserWrapper User { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public string Header { get; set; }

    }

    public class UserWrapper
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }

    }
}
