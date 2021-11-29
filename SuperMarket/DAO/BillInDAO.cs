using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class BillInDAO
    {
        private static BillInDAO instance;
        public static BillInDAO Instance
        {
            get { if (instance == null) instance = new BillInDAO(); return BillInDAO.instance; }
            private set { BillInDAO.instance = value; }
        }
        private BillInDAO()
        { }
        public List<BillIn> GetBillList()
        {
            List<BillIn> billlist = new List<BillIn>();
            string query = "SELECT * FROM dbo.BillIN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                BillIn bill = new BillIn(item);
                billlist.Add(bill);
            }
            return billlist;
        }
        public bool InsertBill(string nameGoods, int price, int count, int total, string date)
        {
            try
            {
                string query = String.Format("INSERT dbo.BillIN (nameGoods,priceOut,countGoods,total,dateBill) " +
                    "VALUES (N'{0}', {1}, {2}, {3}, '{4}')", nameGoods, price, count, total, date);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool MergeBill(string nameGoods, int count, int total)
        {
            try
            {
                string query = String.Format("UPDATE dbo.BillIN SET countGoods = countGoods + {0}, total = total + {1} " +
                    "WHERE nameGoods = N'{2}'", count, total, nameGoods);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteBill(string nameGoods)
        {
            try
            {
                string query = String.Format("DELETE dbo.BillIN WHERE nameGoods = N'{0}'", nameGoods);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
