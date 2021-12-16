using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Repositories.Common
{
   public class DbProcedure
    {
        private DbProcedure(string name, params string[] @params)
        {
            Name = name;
            Parameters = @params ?? new string[0];
        }

        internal string Name { get; set; }
        internal string[] Parameters { get; set; }
    }
}
