using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class TypeGoodsDAO
    {
        private static TypeGoodsDAO instance;
        public static TypeGoodsDAO Instance
        {
            get { if (instance == null) instance = new TypeGoodsDAO(); return TypeGoodsDAO.instance; }
            private set { TypeGoodsDAO.instance = value; }
        }
        private TypeGoodsDAO()
        { }

        public List<TypeGoods> GetTypeGoodsList()
        {
            List<TypeGoods> shifttype = new List<TypeGoods>();
            string query = "SELECT * FROM dbo.TypeGoods";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TypeGoods type = new TypeGoods(item);
                shifttype.Add(type);
            }
            return shifttype;
        }
    }
}
