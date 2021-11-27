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
        private string nameGoods;

        public int Id { get => id; set => id = value; }
        public string NameSupplier { get => nameSupplier; set => nameSupplier = value; }
        public string AddressSupplier { get => addressSupplier; set => addressSupplier = value; }
        public string PhoneSupplier { get => phoneSupplier; set => phoneSupplier = value; }
        public string NameGoods { get => nameGoods; set => nameGoods = value; }

        public Supplier(int id, string nameSupplier, string addressSupplier, string phoneSupplier, string nameGoods)
        {
            this.Id = id;
            this.NameSupplier = nameSupplier;
            this.AddressSupplier = addressSupplier;
            this.PhoneSupplier = phoneSupplier;
            this.NameGoods = nameGoods;
        }
        public Supplier(DataRow row)
        {
            this.Id = (int)row["id"];
            this.NameSupplier = row["nameSupplier"].ToString();
            this.AddressSupplier = row["addressSupplier"].ToString();
            this.PhoneSupplier = row["phoneSupplier"].ToString();
            this.NameGoods = row["nameGoods"].ToString();
        }
    }
}
