using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class TotalRevenue
    {
        private int id;
        private string nameGoods;
        private int priceOut;
        private int countGoods;
        private int total;
        private DateTime dateBill;

        public int Id { get => id; set => id = value; }
        public string NameGoods { get => nameGoods; set => nameGoods = value; }
        public int PriceOut { get => priceOut; set => priceOut = value; }
        public int CountGoods { get => countGoods; set => countGoods = value; }
        public int Total { get => total; set => total = value; }
        public DateTime DateBill { get => dateBill; set => dateBill = value; }

        public TotalRevenue(int id, string nameGoods, int priceOut, int countGoods, int total, DateTime dateBill)
        {
            this.Id = id;
            this.NameGoods = nameGoods;
            this.PriceOut = priceOut;
            this.CountGoods = countGoods;
            this.Total = total;
            this.DateBill = dateBill;
        }
        public TotalRevenue(DataRow row)
        {
            this.Id = (int)row["idBill"];
            this.NameGoods = row["nameGoods"].ToString();
            this.PriceOut = (int)row["priceOut"];
            this.CountGoods = (int)row["countGoods"];
            this.Total = (int)row["total"];
            this.DateBill = Convert.ToDateTime(row["dateBill"]);
        }
    }
}
