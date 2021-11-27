using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class ShiftStaffDAO
    {
        private static ShiftStaffDAO instance;
        public static ShiftStaffDAO Instance
        {
            get { if (instance == null) instance = new ShiftStaffDAO(); return ShiftStaffDAO.instance; }
            private set { ShiftStaffDAO.instance = value; }
        }
        private ShiftStaffDAO()
        { }
        public List<ShiftStaff> GetShiftList()
        {
            List<ShiftStaff> shiftlist = new List<ShiftStaff>();
            string query = "SELECT * FROM dbo.ShiftStaff ORDER BY id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ShiftStaff r = new ShiftStaff(item);
                shiftlist.Add(r);
            }
            return shiftlist;
        }
    }
}
