using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class TotalRevenueDAO
    {
        private static TotalRevenueDAO instance;
        public static TotalRevenueDAO Instance
        {
            get { if (instance == null) instance = new TotalRevenueDAO(); return TotalRevenueDAO.instance; }
            private set { TotalRevenueDAO.instance = value; }
        }
        private TotalRevenueDAO()
        { }
        public List<TotalRevenue> GetTotalRevenueList()
        {
            List<TotalRevenue> list = new List<TotalRevenue>();
            string query = "SELECT * FROM dbo.TotalRevenue";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TotalRevenue revenue = new TotalRevenue(item);
                list.Add(revenue);
            }
            return list;
        }
        public bool PayRevenue()
        {
            try
            {
                string query = "INSERT INTO dbo.TotalRevenue(nameGoods,priceOut,countGoods,total,dateBill) " +
                    "SELECT nameGoods,priceOut,countGoods,total,dateBill " +
                    "FROM dbo.BillIN " +
                    "GO " +
                    "DELETE FROM dbo.BillIN";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        //public bool DeleteBill(string nameGoods)
        //{
        //    try
        //    {
        //        string query = String.Format("DELETE dbo.BillIN WHERE nameGoods = N'{0}'", nameGoods);
        //        int result = DataProvider.Instance.ExecuteNonQuery(query);
        //        return result > 0;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
