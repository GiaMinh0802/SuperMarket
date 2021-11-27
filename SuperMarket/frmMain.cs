using SuperMarket.DAO;
using SuperMarket.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SuperMarket
{
    public partial class fMain : Form
    {
        BindingSource listKH = new BindingSource();
        BindingSource listNV = new BindingSource();
        BindingSource listNCC = new BindingSource();
        public fMain()
        {
            InitializeComponent();
            LoadAll();
        }
        void LoadAll()
        {
            dtgvKH.DataSource = listKH;
            dtgvNV.DataSource = listNV;
            dtgvNCC.DataSource = listNCC;
            // Khach hang
            LoadListKH();
            AddKHBinding();
            LoadListNV();
            // Nhan vien
            LoadListNV();
            AddNVBinding();
            LoadOffice(cbOfficeNV);
            LoadShift(cbShiftNV);
            // Nha cung cap
            LoadListNCC();
            AddNCCBinding();
        }
        #region Khach hang
        void LoadListKH()
        {
            listKH.DataSource = CustomerDAO.Instance.GetKHList();
            dtgvKH.Columns["IDCustomer"].HeaderText = "ID";
            dtgvKH.Columns["NameCustomer"].HeaderText = "Tên khách hàng";
            dtgvKH.Columns["AddressCustomer"].HeaderText = "Địa chỉ";
            dtgvKH.Columns["PhoneNumCustomer"].HeaderText = "SĐT";
            dtgvKH.Columns["BirthDayCustomer"].HeaderText = "Ngày sinh";
            dtgvKH.Columns["AccumulatedPoints"].HeaderText = "Tổng tiền";
            dtgvKH.Columns["Rank"].HeaderText = "Thành viên";
        }
        void AddKHBinding()
        {
            textIDKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "IDCustomer", true, DataSourceUpdateMode.Never));
            textNameKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "NameCustomer", true, DataSourceUpdateMode.Never));
            textAddKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "AddressCustomer", true, DataSourceUpdateMode.Never));
            textSDTKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "PhoneNumCustomer", true, DataSourceUpdateMode.Never));
            birthKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "BirthDayCustomer", true, DataSourceUpdateMode.Never));
            textTotalKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "AccumulatedPoints", true, DataSourceUpdateMode.Never));
            textRankKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "Rank", true, DataSourceUpdateMode.Never));
        }
        string RankByPoints(int point)
        {
            if (point < 500000)
                return "Không";
            else if (point >= 500000 && point < 2000000)
                return "Đồng";
            else if (point >= 2000000 && point < 5000000)
                return "Bạc";
            else if (point >= 5000000 && point < 10000000)
                return "Vàng";
            else
                return "Kim Cương";
        }
        List<Customer> SearchCustomerByName(string name)
        {
            List<Customer> listCustomer = CustomerDAO.Instance.SearchCustomerByName(name);
            return listCustomer;
        }
        private void btnAddKH_Click(object sender, EventArgs e)
        {
            string name = textNameKH.Text;
            string add = textAddKH.Text;
            string phone = textSDTKH.Text;
            string birthday = birthKH.Value.ToString("yyyMMdd");
            int points = Convert.ToInt32(textTotalKH.Text);
            string rank = RankByPoints(points);
            if (CustomerDAO.Instance.InsertCustomer(name, add, phone, birthday, points, rank))
            {
                MessageBox.Show("Thêm thành công");
                LoadListKH();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnUpdateKH_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDKH.Text);
            string name = textNameKH.Text;
            string add = textAddKH.Text;
            string phone = textSDTKH.Text;
            string birthday = birthKH.Value.ToString("yyyMMdd");
            int points = Convert.ToInt32(textTotalKH.Text);
            string rank = RankByPoints(points);
            if (CustomerDAO.Instance.UpdateCustomer(id, name, add, phone, birthday, points, rank))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadListKH();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnDeleteKH_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDKH.Text);
            if (CustomerDAO.Instance.DeleteCustomer(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListKH();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnSearchKH_Click(object sender, EventArgs e)
        {
            listKH.DataSource = SearchCustomerByName(textSearchKH.Text);
        }
        private void btnrefreshKH_Click(object sender, EventArgs e)
        {
            LoadListKH();
        }
        #endregion
        #region Nhan vien
        void LoadListNV()
        {
            listNV.DataSource = StaffDAO.Instance.GetNVList();
            dtgvNV.Columns["IdStaff"].HeaderText = "ID";
            dtgvNV.Columns["NameStaff"].HeaderText = "Tên nhân viên";
            dtgvNV.Columns["IDIndividualStaff"].HeaderText = "CCCD/CMND";
            dtgvNV.Columns["PhoneStaff"].HeaderText = "SĐT";
            dtgvNV.Columns["AddressStaff"].HeaderText = "Địa chỉ";
            dtgvNV.Columns["BirthdayStaff"].HeaderText = "Ngày sinh";
            dtgvNV.Columns["SexStaff"].HeaderText = "Giới tính";
            dtgvNV.Columns["OfficeStaff"].HeaderText = "Chức vụ";
            dtgvNV.Columns["ShiftStaff"].HeaderText = "Ca làm";
            dtgvNV.Columns["SalaryStaff"].HeaderText = "Lương cơ bản";
        }
        void AddNVBinding()
        {
            textIDNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "IdStaff", true, DataSourceUpdateMode.Never));
            textNameNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "NameStaff", true, DataSourceUpdateMode.Never));
            textCCCDNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "IDIndividualStaff", true, DataSourceUpdateMode.Never));
            textSDTNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "PhoneStaff", true, DataSourceUpdateMode.Never));
            textAddNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "AddressStaff", true, DataSourceUpdateMode.Never));
            birthNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "BirthdayStaff", true, DataSourceUpdateMode.Never));
            textSexNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "SexStaff", true, DataSourceUpdateMode.Never));
            cbOfficeNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "OfficeStaff", true, DataSourceUpdateMode.Never));
            cbShiftNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "ShiftStaff", true, DataSourceUpdateMode.Never));
            textSalaryNV.DataBindings.Add(new Binding("Text", dtgvNV.DataSource, "SalaryStaff", true, DataSourceUpdateMode.Never));
        }
        void LoadOffice(ComboBox cb)
        {
            cb.DataSource = OfficeStaffDAO.Instance.GetOfficeList();
            cb.DisplayMember = "Officestaff";
        }
        void LoadShift(ComboBox cb)
        {
            cb.DataSource = ShiftStaffDAO.Instance.GetShiftList();
            cb.DisplayMember = "Shiftstaff";
        }
        List<Staff> GetNVListByShift(string shift)
        {
            List<Staff> listStaff = StaffDAO.Instance.GetNVListByShift(shift);
            return listStaff;
        }
        List<Staff> SearchStaffByName(string name)
        {
            List<Staff> listStaff = StaffDAO.Instance.SearchStaffByName(name);
            return listStaff;
        }
        int SalaryByOffice(string office)
        {
            if (office == "Quản lí")
                return 11000000;
            else if (office == "Quản lí kho")
                return 10000000;
            else if (office == "Vệ sinh")
                return 18000;
            else if (office == "Giữ xe")
                return 15000;
            else if (office == "Thu ngân")
                return 22000;
            else if (office == "Bảo vệ")
                return 21000;
            else
                return 16000;
        }
        private void btnRefreshNV_Click(object sender, EventArgs e)
        {
            LoadListNV();
        }
        private void btnCa1_Click(object sender, EventArgs e)
        {
            string shift = "Ca 1";
            listNV.DataSource = GetNVListByShift(shift);
        }
        private void btnCa2_Click(object sender, EventArgs e)
        {
            string shift = "Ca 2";
            listNV.DataSource = GetNVListByShift(shift);
        }
        private void btnCa3_Click(object sender, EventArgs e)
        {
            string shift = "Ca 3";
            listNV.DataSource = GetNVListByShift(shift);
        }
        private void btnFulltime_Click(object sender, EventArgs e)
        {
            string shift = "Fulltime";
            listNV.DataSource = GetNVListByShift(shift);
        }
        private void btnAddNV_Click(object sender, EventArgs e)
        {
            string name = textNameNV.Text;
            string idIndividual = textCCCDNV.Text;
            string add = textAddNV.Text;
            string phone = textSDTNV.Text;
            string birthday = birthNV.Value.ToString("yyyMMdd");
            string sex = textSexNV.Text;
            string office = cbOfficeNV.Text;
            string shift = cbShiftNV.Text;
            int salary = SalaryByOffice(office);
            if (StaffDAO.Instance.InsertStaff(name, idIndividual, phone, add, birthday, sex, office, shift, salary))
            {
                MessageBox.Show("Thêm thành công");
                LoadListNV();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnUpdateNV_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDNV.Text);
            string name = textNameNV.Text;
            string idIndividual = textCCCDNV.Text;
            string add = textAddNV.Text;
            string phone = textSDTNV.Text;
            string birthday = birthNV.Value.ToString("yyyMMdd");
            string sex = textSexNV.Text;
            string office = cbOfficeNV.Text;
            string shift = cbShiftNV.Text;
            int salary = SalaryByOffice(office);
            if (StaffDAO.Instance.UpdateStaff(id, name, idIndividual, phone, add, birthday, sex, office, shift, salary))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadListNV();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }

        }
        private void btnDeleteNV_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDNV.Text);
            if (StaffDAO.Instance.DeleteStaff(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListNV();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnSearchNV_Click(object sender, EventArgs e)
        {
            listNV.DataSource = SearchStaffByName(textSearchNV.Text);
        }
        #endregion
        #region Nha cung cap
        void LoadListNCC()
        {
            listNCC.DataSource = SupplierDAO.Instance.GetNCCList();
            dtgvNCC.Columns["Id"].HeaderText = "ID";
            dtgvNCC.Columns["NameSupplier"].HeaderText = "Tên nhà cung cấp";
            dtgvNCC.Columns["AddressSupplier"].HeaderText = "Địa chỉ";
            dtgvNCC.Columns["PhoneSupplier"].HeaderText = "SĐT";
            dtgvNCC.Columns["NameGoods"].HeaderText = "Mặt hàng cung cấp";
        }
        void AddNCCBinding()
        {
            textIDNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "Id", true, DataSourceUpdateMode.Never));
            textNameNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "NameSupplier", true, DataSourceUpdateMode.Never));
            textAddNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "AddressSupplier", true, DataSourceUpdateMode.Never));
            textSDTNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "PhoneSupplier", true, DataSourceUpdateMode.Never));
            textGoodsNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "NameGoods", true, DataSourceUpdateMode.Never));
        }
        List<Supplier> SearchSupplierByName(string name)
        {
            List<Supplier> listSupplier = SupplierDAO.Instance.SearchSupplierByName(name);
            return listSupplier;
        }
        private void btnRefreshNCC_Click(object sender, EventArgs e)
        {
            LoadListNCC();
        }
        private void btnAddNCC_Click(object sender, EventArgs e)
        {
            string namesupplier = textNameNCC.Text;
            string add = textAddNCC.Text;
            string phone = textSDTNCC.Text;
            string namegoods = textGoodsNCC.Text;
            if (SupplierDAO.Instance.InsertSupplier(namesupplier, add, phone, namegoods))
            {
                MessageBox.Show("Thêm thành công");
                LoadListNCC();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnUpdateNCC_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDNCC.Text);
            string namesupplier = textNameNCC.Text;
            string add = textAddNCC.Text;
            string phone = textSDTNCC.Text;
            string namegoods = textGoodsNCC.Text;
            if (SupplierDAO.Instance.UpdateSupplier(id, namesupplier, add, phone, namegoods))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadListNCC();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnDeleteNCC_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDNCC.Text);
            if (SupplierDAO.Instance.DeleteSupplier(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListNCC();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnSearchNCC_Click(object sender, EventArgs e)
        {
            listNCC.DataSource = SearchSupplierByName(textSearchNCC.Text);
        }
        #endregion
        #region 
    }
}
