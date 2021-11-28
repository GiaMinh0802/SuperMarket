﻿using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DAO
{
    class GoodsDAO
    {
        private static GoodsDAO instance;
        public static GoodsDAO Instance
        {
            get { if (instance == null) instance = new GoodsDAO(); return GoodsDAO.instance; }
            private set { GoodsDAO.instance = value; }
        }
        private GoodsDAO()
        { }
        public List<Goods> GetGoodsList()
        {
            List<Goods> goodslist = new List<Goods>();
            string query = "SELECT * FROM dbo.Goods";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Goods goods = new Goods(item);
                goodslist.Add(goods);
            }
            return goodslist;
        }
        public bool InsertGoods(string name, string type, int priceIn, int priceOut, string exp, string mfg, int quantity, string namesupplier)
        {
            string query = String.Format("INSERT dbo.Goods(nameGoods,typeGoods,priceIn,priceOut,EXPGoods,MFGGoods,quantityGoods,nameSupplier,VAT) " +
                "VALUES (N'{0}', N'{1}', {2}, {3}, '{4}', '{5}', {6}, N'{7}', DEFAULT)", name, type, priceIn, priceOut, exp, mfg, quantity, namesupplier);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteGoods(int id)
        {
            string query = String.Format("DELETE dbo.Goods WHERE idGoods = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteGoodsByNameSupplier(string supplier)
        {
            string query = String.Format("DELETE dbo.Goods WHERE nameSupplier = N'{0}'", supplier);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateGoods(int id, string name, string type, int priceIn, int priceOut, string exp, string mfg, int quantity, string namesupplier)
        {
            string query = String.Format("UPDATE dbo.Goods SET nameGoods = N'{0}', typeGoods = N'{1}', priceIn = {2}, priceOut = {3}, EXPGoods = '{4}', MFGGoods = '{5}', quantityGoods = {6}, nameSupplier = N'{7}' " +
                "WHERE idGoods = {8}", name, type, priceIn, priceOut, exp, mfg, quantity, namesupplier, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public List<Goods> SearchGoodsByType(string type)
        {
            List<Goods> list = new List<Goods>();
            string query = String.Format("SELECT * FROM dbo.Goods WHERE typeGoods = N'{0}'", type);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Goods goods = new Goods(item);
                list.Add(goods);
            }
            return list;
        }
        public List<Goods> SearchGoodsByName(string name)
        {
            List<Goods> list = new List<Goods>();
            string query = String.Format("SELECT * FROM dbo.Goods WHERE dbo.fuConvertToUnsign1(nameGoods) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Goods goods = new Goods(item);
                list.Add(goods);
            }
            return list;
        }
    }
}
