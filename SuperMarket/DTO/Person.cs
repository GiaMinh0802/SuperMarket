using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class Person
    {
        private int id;
        private string name;
        private string address;
        private string phone;
        private DateTime birthDay;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime BirthDay { get => birthDay; set => birthDay = value; }
        
        public Person()
        { }
        public Person(int id, string name, string address, string phone, DateTime birthDay)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.Phone = phone;
            this.BirthDay = birthDay;
        }
        ~Person()
        { }
    }
}
