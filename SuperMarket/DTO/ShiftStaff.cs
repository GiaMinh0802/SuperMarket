using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.DTO
{
    class ShiftStaff
    {
        private int id;
        private string shiftstaff;
        private int totalTime;

        public int Id { get => id; set => id = value; }
        public int TotalTime { get => totalTime; set => totalTime = value; }
        public string Shiftstaff { get => shiftstaff; set => shiftstaff = value; }

        public ShiftStaff(int id, string shiftStaff, int totalTime)
        {
            this.Id = id;
            this.Shiftstaff = Shiftstaff;
            this.TotalTime = totalTime;
        }
        public ShiftStaff(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Shiftstaff = row["shiftStaff"].ToString();
            this.TotalTime = (int)row["totalTime"];
        }
    }
}
