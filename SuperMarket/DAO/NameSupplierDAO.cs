using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class NameSupplierDAO
    {
        private static NameSupplierDAO instance;
        public static NameSupplierDAO Instance
        {
            get { if (instance == null) instance = new NameSupplierDAO(); return NameSupplierDAO.instance; }
            private set { NameSupplierDAO.instance = value; }
        }
        private NameSupplierDAO()
        { }
        public List<NameSupplier> GetListNameSupplier()
        {
            List<NameSupplier> supplierlist = new List<NameSupplier>();
            string query = "SELECT nameSupplier FROM dbo.Supplier";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                NameSupplier supp = new NameSupplier(item);
                supplierlist.Add(supp);
            }
            return supplierlist;
        }
    }
}
