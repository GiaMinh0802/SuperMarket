using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class CustomerDAO
    {
        private static CustomerDAO instance;
        public static CustomerDAO Instance
        {
            get { if (instance == null) instance = new CustomerDAO(); return CustomerDAO.instance; }
            private set { CustomerDAO.instance = value; }
        }
        private CustomerDAO()
        { }
        public List<Customer> GetKHList()
        {
            List<Customer> customerlist = new List<Customer>();
            string query = "SELECT * FROM dbo.Customer";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Customer cus = new Customer(item);
                customerlist.Add(cus);
            }    
            return customerlist;
        }
        public bool InsertCustomer(string name, string add, string phone, string birthday, int points, string rank)
        {
            string query = String.Format("INSERT dbo.Customer (nameCustomer,phoneCustomer,addressCustomer,birthdayCustomer,accumulatedPoints,rankCustomer) " +
                "VALUES (N'{0}', '{1}', N'{2}', '{3}', {4}, N'{5}')", name, phone, add, birthday, points, rank);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result>0;
        }
        public bool UpdateCustomer(int id, string name, string add, string phone, string birthday, int points, string rank)
        {
            string query = String.Format("UPDATE dbo.Customer SET nameCustomer = N'{0}', phoneCustomer = '{1}', addressCustomer = N'{2}', birthdayCustomer = '{3}', accumulatedPoints = {4}, rankCustomer = N'{5}' " +
                "WHERE idCustomer = {6}", name, phone, add, birthday, points, rank, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteCustomer(int id)
        {
            string query = String.Format("DELETE dbo.Customer WHERE idCustomer = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public List<Customer> SearchCustomerByName(string name)
        {
            List<Customer> list = new List<Customer>();
            string query = String.Format("SELECT * FROM dbo.Customer WHERE dbo.fuConvertToUnsign1(nameCustomer) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Customer cus = new Customer(item);
                list.Add(cus);
            }
            return list;
        }
    }
}
