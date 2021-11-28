using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class Goods
    {
        private int idGoods;
        private string nameGoods;
        private string typeGoods;
        private int priceIn;
        private int priceOut;
        private DateTime expGoods;
        private DateTime mfgGoods;
        private int quantityGoods;
        private string nameSupplier;
        private int vat;

        public int IdGoods { get => idGoods; set => idGoods = value; }
        public string NameGoods { get => nameGoods; set => nameGoods = value; }
        public string TypeGoods { get => typeGoods; set => typeGoods = value; }
        public int PriceIn { get => priceIn; set => priceIn = value; }
        public int PriceOut { get => priceOut; set => priceOut = value; }
        public DateTime ExpGoods { get => expGoods; set => expGoods = value; }
        public DateTime MfgGoods { get => mfgGoods; set => mfgGoods = value; }
        public int QuantityGoods { get => quantityGoods; set => quantityGoods = value; }
        public string NameSupplier { get => nameSupplier; set => nameSupplier = value; }
        public int VAT { get => vat; set => vat = value; }

        public Goods(int idGoods, string nameGoods, string typeGoods, int priceIn, int priceOut, DateTime expGoods, DateTime mfgGoods, int quantityGoods, string nameSupplier, int vat)
        {
            this.IdGoods = idGoods;
            this.NameGoods = nameGoods;
            this.TypeGoods = typeGoods;
            this.PriceIn = priceIn;
            this.PriceOut = priceOut;
            this.ExpGoods = expGoods;
            this.MfgGoods = mfgGoods;
            this.QuantityGoods = quantityGoods;
            this.NameSupplier = nameSupplier;
            this.VAT = vat;
        }
        public Goods(DataRow row)
        {
            this.IdGoods = (int)row["idGoods"];
            this.NameGoods = row["nameGoods"].ToString();
            this.TypeGoods = row["typeGoods"].ToString();
            this.PriceIn = (int)row["priceIn"];
            this.PriceOut = (int)row["priceOut"];
            this.ExpGoods = Convert.ToDateTime(row["EXPGoods"]);
            this.MfgGoods = Convert.ToDateTime(row["MFGGoods"]);
            this.QuantityGoods = (int)row["quantityGoods"];
            this.NameSupplier = row["nameSupplier"].ToString();
            this.VAT = (int)row["VAT"];
        }
    }
}
