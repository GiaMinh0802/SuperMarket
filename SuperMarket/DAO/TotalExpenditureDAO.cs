using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class TotalExpenditureDAO
    {
        private static TotalExpenditureDAO instance;
        public static TotalExpenditureDAO Instance
        {
            get { if (instance == null) instance = new TotalExpenditureDAO(); return TotalExpenditureDAO.instance; }
            private set { TotalExpenditureDAO.instance = value; }
        }
        private TotalExpenditureDAO()
        { }
        public List<TotalExpenditure> GetTotalExpenditureList()
        {
            List<TotalExpenditure> list = new List<TotalExpenditure>();
            string query = "SELECT * FROM dbo.TotalExpenditure " +
                "ORDER BY typeBill";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TotalExpenditure expenditure = new TotalExpenditure(item);
                list.Add(expenditure);
            }
            return list;
        }
        public List<TotalExpenditure> GetExpenditureByDate(string daystart, string dayfinish)
        {
            List<TotalExpenditure> list = new List<TotalExpenditure>();
            string query = String.Format("SELECT * FROM dbo.TotalExpenditure " +
                "WHERE dateBill >= '{0}' AND dateBill <= '{1}' " +
                "ORDER BY typeBill", daystart, dayfinish);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TotalExpenditure expenditure = new TotalExpenditure(item);
                list.Add(expenditure);
            }
            return list;
        }
        public bool InsertExpenditure(string type, string namegoods, int price, int count, int total, string day)
        {
            try
            {
                string query = String.Format("INSERT dbo.TotalExpenditure (typeBill,nameGoods,priceOut,countGoods,total,dateBill) " +
                    "VALUES (N'{0}', N'{1}', {2}, {3}, {4}, '{5}')", type, namegoods, price, count, total, day);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool RefreshExpenditure()
        {
            try
            {
                string query = "SELECT typeBill, nameGoods, priceOut, SUM(countGoods) AS countGoods, SUM(total) AS total, dateBill " +
                "INTO #temp " +
                "FROM dbo.TotalExpenditure " +
                "GROUP BY typeBill, nameGoods, priceOut, dateBill " +
                "DELETE FROM dbo.TotalExpenditure " +
                "INSERT INTO dbo.TotalExpenditure(typeBill,nameGoods,priceOut,countGoods,total,dateBill) " +
                "SELECT typeBill, nameGoods, priceOut, countGoods, total, dateBill " +
                "FROM #temp " +
                "DROP TABLE #temp";
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
