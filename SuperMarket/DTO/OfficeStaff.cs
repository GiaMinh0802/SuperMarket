using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class OfficeStaff
    {
        private int id;
        private string officestaff;
        private int salarystaff;

        public int Id { get => id; set => id = value; }
        public string Officestaff { get => officestaff; set => officestaff = value; }
        public int Salarystaff { get => salarystaff; set => salarystaff = value; }

        public OfficeStaff(int id, string officestaff, int salarystaff)
        {
            this.Id = id;
            this.Officestaff = officestaff;
            this.Salarystaff = salarystaff;
        }
        public OfficeStaff(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Officestaff = row["officeStaff"].ToString();
            this.Salarystaff = (int)row["salaryStaff"];
        }
    }
}
