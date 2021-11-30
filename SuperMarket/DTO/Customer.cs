using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class Customer
    {
        private int idCustomer;
        private string nameCustomer;
        private string addressCustomer;
        private string phoneNumCustomer;
        private DateTime birthDayCustomer;
        private int accumulatedPoints;
        private string rank;
        public int IDCustomer { get => idCustomer; set => idCustomer = value; }
        public string NameCustomer { get => nameCustomer; set => nameCustomer = value; }
        public string PhoneNumCustomer { get => phoneNumCustomer; set => phoneNumCustomer = value; }
        public string AddressCustomer { get => addressCustomer; set => addressCustomer = value; }
        public DateTime BirthDayCustomer { get => birthDayCustomer; set => birthDayCustomer = value; }
        public int AccumulatedPoints { get => accumulatedPoints; set => accumulatedPoints = value; }
        public string Rank { get => rank; set => rank = value; }
        public Customer(int idCustomer, string nameCustomer, string addressCustomer, string phoneNumCustomer, DateTime birthDayCustomer, int accumulatedPoints, string rank)
        {
            this.IDCustomer = idCustomer;
            this.NameCustomer = nameCustomer;
            this.AddressCustomer = addressCustomer;
            this.PhoneNumCustomer = phoneNumCustomer;
            this.BirthDayCustomer = birthDayCustomer;
            this.AccumulatedPoints = accumulatedPoints;
            this.Rank = rank;
        }
        public Customer(DataRow row)
        {
            this.IDCustomer = (int)row["idCustomer"];
            this.NameCustomer = row["nameCustomer"].ToString();
            this.PhoneNumCustomer = row["phoneCustomer"].ToString();
            this.AddressCustomer = row["addressCustomer"].ToString();
            this.BirthDayCustomer = Convert.ToDateTime(row["birthdayCustomer"]);
            this.AccumulatedPoints = (int)row["accumulatedPoints"];
            this.Rank = row["rankCustomer"].ToString();
        }
    }
}
