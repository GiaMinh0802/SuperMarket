using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class Staff : Person
    {
        private string iDIndividualStaff;
        private string sexStaff;
        private string officeStaff;
        private string shiftStaff;
        private int salaryStaff;

        public string IDIndividualStaff { get => iDIndividualStaff; set => iDIndividualStaff = value; }
        public string OfficeStaff { get => officeStaff; set => officeStaff = value; }
        public string ShiftStaff { get => shiftStaff; set => shiftStaff = value; }
        public string SexStaff { get => sexStaff; set => sexStaff = value; }
        public int SalaryStaff { get => salaryStaff; set => salaryStaff = value; }

        public Staff(int id, string name, string iDIndividualStaff, string phone, string address, DateTime birthDay, string sexStaff, string officeStaff, string shiftStaff, int salaryStaff) : base(id,name,address,phone,birthDay)
        {
            this.IDIndividualStaff = iDIndividualStaff;
            this.SexStaff = sexStaff;
            this.OfficeStaff = officeStaff;
            this.ShiftStaff = shiftStaff;
            this.SalaryStaff = salaryStaff;
        }
        public Staff(DataRow row)
        {
            this.Id = (int)row["idStaff"];
            this.Name = row["nameStaff"].ToString();
            this.IDIndividualStaff = row["iDIndividualStaff"].ToString();
            this.Phone = row["phoneStaff"].ToString();
            this.Address = row["addressStaff"].ToString();
            this.BirthDay = Convert.ToDateTime(row["birthdayStaff"]);
            this.SexStaff = row["sexStaff"].ToString();
            this.OfficeStaff = row["officeStaff"].ToString();
            this.ShiftStaff = row["shiftStaff"].ToString();
            this.SalaryStaff = (int)row["salaryStaff"];
        }
    }
}
