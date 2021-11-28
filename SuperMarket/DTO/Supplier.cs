using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class Supplier
    {
        private int id;
        private string nameSupplier;
        private string addressSupplier;
        private string phoneSupplier;

        public int Id { get => id; set => id = value; }
        public string NameSupplier { get => nameSupplier; set => nameSupplier = value; }
        public string AddressSupplier { get => addressSupplier; set => addressSupplier = value; }
        public string PhoneSupplier { get => phoneSupplier; set => phoneSupplier = value; }

        public Supplier(int id, string nameSupplier, string addressSupplier, string phoneSupplier)
        {
            this.Id = id;
            this.NameSupplier = nameSupplier;
            this.AddressSupplier = addressSupplier;
            this.PhoneSupplier = phoneSupplier;
        }
        public Supplier(DataRow row)
        {
            this.Id = (int)row["id"];
            this.NameSupplier = row["nameSupplier"].ToString();
            this.AddressSupplier = row["addressSupplier"].ToString();
            this.PhoneSupplier = row["phoneSupplier"].ToString();
        }
    }
}
