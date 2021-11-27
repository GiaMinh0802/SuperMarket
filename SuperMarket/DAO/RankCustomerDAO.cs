using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class RankCustomerDAO
    {
        private static RankCustomerDAO instance;
        public static RankCustomerDAO Instance
        {
            get { if (instance == null) instance = new RankCustomerDAO(); return RankCustomerDAO.instance; }
            private set { RankCustomerDAO.instance = value; }
        }
        private RankCustomerDAO()
        { }
        public List<RankCustomer> GetRankList()
        {
            List<RankCustomer> ranklist = new List<RankCustomer>();
            string query = "SELECT * FROM dbo.RankCustomer ORDER BY idRank";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RankCustomer r = new RankCustomer(item);
                ranklist.Add(r);
            }
            return ranklist;
        }
    }
}
