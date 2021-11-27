using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class OfficeStaffDAO
    {
        private static OfficeStaffDAO instance;
        public static OfficeStaffDAO Instance
        {
            get { if (instance == null) instance = new OfficeStaffDAO(); return OfficeStaffDAO.instance; }
            private set { OfficeStaffDAO.instance = value; }
        }
        private OfficeStaffDAO()
        { }
        public List<OfficeStaff> GetOfficeList()
        {
            List<OfficeStaff> officelist = new List<OfficeStaff>();
            string query = "SELECT * FROM dbo.OfficeStaff ORDER BY id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                OfficeStaff r = new OfficeStaff(item);
                officelist.Add(r);
            }
            return officelist;
        }
    }
}
