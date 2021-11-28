using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class NameSupplier
    {
        private string namesupplier;

        public string Namesupplier { get => namesupplier; set => namesupplier = value; }
    
        public NameSupplier(string namesupplier)
        {
            this.Namesupplier = namesupplier;
        }
        public NameSupplier(DataRow row)
        {
            this.Namesupplier = row["nameSupplier"].ToString();
        }
    }
}
