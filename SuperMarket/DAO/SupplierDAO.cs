using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class SupplierDAO
    {
        private static SupplierDAO instance;
        public static SupplierDAO Instance
        {
            get { if (instance == null) instance = new SupplierDAO(); return SupplierDAO.instance; }
            private set { SupplierDAO.instance = value; }
        }
        private SupplierDAO()
        { }
        public List<Supplier> GetNCCList()
        {
            List<Supplier> supplierlist = new List<Supplier>();
            string query = "SELECT * FROM dbo.Supplier ORDER BY id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Supplier supp = new Supplier(item);
                supplierlist.Add(supp);
            }
            return supplierlist;
        }
        public bool InsertSupplier(string nameSupplier, string address, string phone, string nameGoods)
        {
            string query = String.Format("INSERT dbo.Supplier (nameSupplier,addressSupplier,phoneSupplier,nameGoods) " +
                "VALUES (N'{0}', N'{1}', '{2}', N'{3}')", nameSupplier, address, phone, nameGoods);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateSupplier(int id, string nameSupplier, string address, string phone, string nameGoods)
        {
            string query = String.Format("UPDATE dbo.Supplier SET nameSupplier = N'{0}', addressSupplier = N'{1}', phoneSupplier = '{2}', nameGoods = N'{3}' " +
                "WHERE id = {4}", nameSupplier, address, phone, nameGoods, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteSupplier(int id)
        {
            string query = String.Format("DELETE dbo.Supplier WHERE id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public List<Supplier> SearchSupplierByName(string name)
        {
            List<Supplier> list = new List<Supplier>();
            string query = String.Format("SELECT * FROM dbo.Supplier WHERE dbo.fuConvertToUnsign1(nameSupplier) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Supplier supp = new Supplier(item);
                list.Add(supp);
            }
            return list;
        }
    }
}
