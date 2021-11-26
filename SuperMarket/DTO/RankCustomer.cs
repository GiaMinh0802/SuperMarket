using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class RankCustomer
    {
        private string rank;
        private int idrank;

        public string Rank { get => rank; set => rank = value; }
        public int Idrank { get => idrank; set => idrank = value; }

        public RankCustomer(int idrank, string rank)
        {
            this.Idrank = idrank;
            this.Rank = rank;
        }
        public RankCustomer(DataRow row)
        {
            this.Idrank = (int)row["idRank"];
            this.Rank = row["rankCustomer"].ToString();
        }
    }
}
