using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class TypeGoods
    {
        private int id;
        private string typegoods;

        public int Id { get => id; set => id = value; }
        public string Typegoods { get => typegoods; set => typegoods = value; }
    
        public TypeGoods(int id, string typegoods)
        {
            this.Id = id;
            this.Typegoods = typegoods;
        }
        public TypeGoods(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Typegoods = row["typeGoods"].ToString();
        }
    }
}
