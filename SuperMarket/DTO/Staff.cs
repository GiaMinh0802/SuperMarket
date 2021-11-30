using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class Staff
    {
        private int idStaff;
        private string nameStaff;
        private string iDIndividualStaff;
        private string phoneStaff;
        private string addressStaff;
        private DateTime birthdayStaff;
        private string sexStaff;
        private string officeStaff;
        private string shiftStaff;
        private int salaryStaff;

        public int IdStaff { get => idStaff; set => idStaff = value; }
        public string NameStaff { get => nameStaff; set => nameStaff = value; }
        public string IDIndividualStaff { get => iDIndividualStaff; set => iDIndividualStaff = value; }
        public string PhoneStaff { get => phoneStaff; set => phoneStaff = value; }
        public string OfficeStaff { get => officeStaff; set => officeStaff = value; }
        public string AddressStaff { get => addressStaff; set => addressStaff = value; }
        public DateTime BirthdayStaff { get => birthdayStaff; set => birthdayStaff = value; }
        public string SexStaff { get => sexStaff; set => sexStaff = value; }
        public string ShiftStaff { get => shiftStaff; set => shiftStaff = value; }
        public int SalaryStaff { get => salaryStaff; set => salaryStaff = value; }

        public Staff(int idStaff, string nameStaff, string iDIndividualStaff, string phoneStaff, string addressStaff, DateTime birthdayStaff, string sexStaff, string officeStaff, string shiftStaff, int salaryStaff)
        {
            this.IdStaff = idStaff;
            this.NameStaff = nameStaff;
            this.IDIndividualStaff = iDIndividualStaff;
            this.PhoneStaff = phoneStaff;
            this.AddressStaff = AddressStaff;
            this.BirthdayStaff = birthdayStaff;
            this.SexStaff = sexStaff;
            this.OfficeStaff = officeStaff;
            this.ShiftStaff = shiftStaff;
            this.SalaryStaff = salaryStaff;
        }
        public Staff(DataRow row)
        {
            this.IdStaff = (int)row["idStaff"];
            this.NameStaff = row["nameStaff"].ToString();
            this.IDIndividualStaff = row["iDIndividualStaff"].ToString();
            this.PhoneStaff = row["phoneStaff"].ToString();
            this.AddressStaff = row["addressStaff"].ToString();
            this.BirthdayStaff = Convert.ToDateTime(row["birthdayStaff"]);
            this.SexStaff = row["sexStaff"].ToString();
            this.OfficeStaff = row["officeStaff"].ToString();
            this.ShiftStaff = row["shiftStaff"].ToString();
            this.SalaryStaff = (int)row["salaryStaff"];
        }
    }
}
