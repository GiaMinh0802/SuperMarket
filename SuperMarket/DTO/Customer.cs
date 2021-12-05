using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class Customer : Person
    {
        private int accumulatedPoints;
        private string rank;
        public int AccumulatedPoints { get => accumulatedPoints; set => accumulatedPoints = value; }
        public string Rank { get => rank; set => rank = value; }
        public Customer(int id, string name, string address, string phone, DateTime birthDay, int accumulatedPoints, string rank) : base(id,name,address,phone,birthDay)
        {
            this.AccumulatedPoints = accumulatedPoints;
            this.Rank = rank;
        }
        public Customer(DataRow row)
        {
            this.Id = (int)row["idCustomer"];
            this.Name = row["nameCustomer"].ToString();
            this.Phone = row["phoneCustomer"].ToString();
            this.Address = row["addressCustomer"].ToString();
            this.BirthDay = Convert.ToDateTime(row["birthdayCustomer"]);
            this.AccumulatedPoints = (int)row["accumulatedPoints"];
            this.Rank = row["rankCustomer"].ToString();
        }
    }
}
