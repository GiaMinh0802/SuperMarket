using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class StaffDAO
    {
        private static StaffDAO instance;
        public static StaffDAO Instance
        {
            get { if (instance == null) instance = new StaffDAO(); return StaffDAO.instance; }
            private set { StaffDAO.instance = value; }
        }
        private StaffDAO()
        { }

        public List<Staff> GetNVList()
        {
            List<Staff> stafflist = new List<Staff>();
            string query = "SELECT * FROM dbo.Staff";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);
                stafflist.Add(staff);
            }
            return stafflist;
        }
        public List<Staff> GetNVListByShift(string shift)
        {
            List<Staff> stafflist = new List<Staff>();
            string query = String.Format("SELECT * FROM dbo.Staff WHERE shiftStaff = N'{0}'", shift);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);
                stafflist.Add(staff);
            }
            return stafflist;
        }
        public bool InsertStaff(string name, string idIndividual, string phone, string address, string birthday, string sex, string office, string shift, int salary)
        {
            try
            {
                string query = String.Format("INSERT dbo.Staff (nameStaff,iDIndividualStaff,phoneStaff,addressStaff,birthdayStaff,sexStaff,officeStaff,shiftStaff,salaryStaff) " +
                    "VALUES (N'{0}', '{1}', '{2}', N'{3}', '{4}', N'{5}', N'{6}', N'{7}', {8})", name, idIndividual, phone, address, birthday, sex, office, shift, salary);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateStaff(int id, string name, string idIndividual, string phone, string address, string birthday, string sex, string office, string shift, int salary)
        {
            try
            {
                string query = String.Format("UPDATE dbo.Staff SET nameStaff = N'{0}', iDIndividualStaff = '{1}', phoneStaff = '{2}', addressStaff = N'{3}', birthdayStaff = '{4}', sexStaff = N'{5}', officeStaff = N'{6}', shiftStaff = N'{7}', salaryStaff = {8} " +
                "WHERE idStaff = {9}", name, idIndividual, phone, address, birthday, sex, office, shift, salary, id);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteStaff(int id)
        {
            try
            {
                string query = String.Format("DELETE dbo.Staff WHERE idStaff = {0}", id);
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                return result > 0;
            }
            catch
            {
                return false;
            }
        }
        public List<Staff> SearchStaffByName(string name)
        {
            List<Staff> list = new List<Staff>();
            string query = String.Format("SELECT * FROM dbo.Staff WHERE dbo.fuConvertToUnsign1(nameStaff) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);
                list.Add(staff);
            }
            return list;
        }
    }
}
