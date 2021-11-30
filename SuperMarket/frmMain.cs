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
        BindingSource listHH = new BindingSource();
        BindingSource listBill = new BindingSource();
        BindingSource listRevenue = new BindingSource();
        BindingSource listExpenditure = new BindingSource();
        string rank = null;
        int total = 0;
        int discount = 0;
        int totalRevenue = 0;
        int totalExpenditure = 0;
        int totalNH = 0;
        int totalsalaryNV = 0;
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
            dtgvHH.DataSource = listHH;
            dtgvBill.DataSource = listBill;
            dtgvRevenue.DataSource = listRevenue;
            dtgvExpenditure.DataSource = listExpenditure;
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
            LoadNameGoodsNH();
            cbPriceNH.Text = "0";
            cbGoodsNH.Text = null;
            // Hang hoa
            LoadListHH();
            AddHHBinding();
            LoadTypeGoods(cbSearchTypeHH);
            LoadTypeGoods(cbTypeHH);
            TotalNH();
            // Hoa don
            LoadListBill();
            LoadTypeGoodsBill(cbTypeBill);
            LoadCustomerBill(cbSDTKHBill);
            cbSDTKHBill.Text = null;
            cbNameKHBill.Text = null;
            textDiscount.Text = null;
            textPay.Text = null;
            Total();
            // Doanh thu
            LoadListRevenue();
            LoadListExpenditure();
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
            dtgvKH.Columns["BirthDayCustomer"].DefaultCellStyle.Format = "dd/MM/yyy";
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
            if (point < 1000000)
                return "Không";
            else if (point >= 1000000 && point < 50000000)
                return "Đồng";
            else if (point >= 50000000 && point < 200000000)
                return "Bạc";
            else if (point >= 200000000 && point < 500000000)
                return "Vàng";
            else
                return "Kim Cương";
        }
        List<Customer> SearchCustomerByPhone(string phone)
        {
            List<Customer> listCustomer = CustomerDAO.Instance.SearchCustomerByPhone(phone);
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
                LoadCustomerBill(cbSDTKHBill);
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
                LoadCustomerBill(cbSDTKHBill);
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
                LoadCustomerBill(cbSDTKHBill);
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnSearchKH_Click(object sender, EventArgs e)
        {
            listKH.DataSource = SearchCustomerByPhone(textSearchKH.Text);
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
            dtgvNV.Columns["BirthdayStaff"].DefaultCellStyle.Format = "dd/MM/yyy";
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
            textTotalSalaryNV.Text = null;
            btnCa1.BackColor = Color.Transparent;
            btnCa2.BackColor = Color.Transparent;
            btnCa3.BackColor = Color.Transparent;
            btnFulltime.BackColor = Color.Transparent;
        }
        private void btnCa1_Click(object sender, EventArgs e)
        {
            string shift = "Ca 1";
            listNV.DataSource = GetNVListByShift(shift);
            btnCa1.BackColor = Color.LightCoral;
            btnCa2.BackColor = Color.Transparent;
            btnCa3.BackColor = Color.Transparent;
            btnFulltime.BackColor = Color.Transparent;
        }
        private void btnCa2_Click(object sender, EventArgs e)
        {
            string shift = "Ca 2";
            listNV.DataSource = GetNVListByShift(shift);
            btnCa1.BackColor = Color.Transparent;
            btnCa2.BackColor = Color.LightCoral;
            btnCa3.BackColor = Color.Transparent;
            btnFulltime.BackColor = Color.Transparent;
        }
        private void btnCa3_Click(object sender, EventArgs e)
        {
            string shift = "Ca 3";
            listNV.DataSource = GetNVListByShift(shift);
            btnCa1.BackColor = Color.Transparent;
            btnCa2.BackColor = Color.Transparent;
            btnCa3.BackColor = Color.LightCoral;
            btnFulltime.BackColor = Color.Transparent;
        }
        private void btnFulltime_Click(object sender, EventArgs e)
        {
            string shift = "Fulltime";
            listNV.DataSource = GetNVListByShift(shift);
            btnCa1.BackColor = Color.Transparent;
            btnCa2.BackColor = Color.Transparent;
            btnCa3.BackColor = Color.Transparent;
            btnFulltime.BackColor = Color.LightCoral;
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
        private void btnCalcSalary_Click(object sender, EventArgs e)
        {
            int salary = Convert.ToInt32(textSalaryNV.Text);
            string office = cbOfficeNV.Text.ToString();
            string shift = cbShiftNV.Text.ToString();
            totalsalaryNV = 0;
            if (office == "Quản lí")
                totalsalaryNV = 11000000;
            else if (office == "Quản lí kho")
                totalsalaryNV = 10000000;
            else
            {
                if (shift == "Fulltime")
                    totalsalaryNV = (salary * 11 * 30);
                else
                    totalsalaryNV = (salary * 5 * 30);
            }
            textTotalSalaryNV.Text = totalsalaryNV.ToString();
            textTotalSalaryNV.Text = decimal.Parse(textTotalSalaryNV.Text.Replace(",", ".")).ToString("0,0.##");
            if (textTotalSalaryNV.Text == "00")
            {
                textTotalSalaryNV.Text = "0";
            }
        }
        private void btnSalary_Click(object sender, EventArgs e)
        {
            string type = "Trả lương";
            string name = textNameNV.Text;
            int price = Convert.ToInt32(textSalaryNV.Text);
            int count = 1;
            int total = totalsalaryNV;
            string day = DateTime.Today.ToString("yyyMMdd");
            if (TotalExpenditureDAO.Instance.InsertExpenditure(type,name,price,count,total,day))
            {
                MessageBox.Show("Trả lương thành công");
                LoadListExpenditure();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        #endregion
        #region Nha cung cap
        void LoadListNCC()
        {
            listNCC.DataSource = SupplierDAO.Instance.GetNCCList();
            cbNameSupplierHH.DataSource = NameSupplierDAO.Instance.GetListNameSupplier();
            cbNameSupplierHH.DisplayMember = "Namesupplier";
            dtgvNCC.Columns["Id"].HeaderText = "ID";
            dtgvNCC.Columns["NameSupplier"].HeaderText = "Tên nhà cung cấp";
            dtgvNCC.Columns["AddressSupplier"].HeaderText = "Địa chỉ";
            dtgvNCC.Columns["PhoneSupplier"].HeaderText = "SĐT";
        }
        void AddNCCBinding()
        {
            textIDNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "Id", true, DataSourceUpdateMode.Never));
            textNameNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "NameSupplier", true, DataSourceUpdateMode.Never));
            textAddNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "AddressSupplier", true, DataSourceUpdateMode.Never));
            textSDTNCC.DataBindings.Add(new Binding("Text", dtgvNCC.DataSource, "PhoneSupplier", true, DataSourceUpdateMode.Never));
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
            if (SupplierDAO.Instance.InsertSupplier(namesupplier, add, phone))
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
            if (SupplierDAO.Instance.UpdateSupplier(id, namesupplier, add, phone))
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
            string namesupplier = textNameNCC.Text;
            GoodsDAO.Instance.DeleteGoodsByNameSupplier(namesupplier);
            LoadListHH();
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
            listNCC.DataSource = SupplierDAO.Instance.SearchSupplierByName(textSearchNCC.Text);
        }
        void LoadNameGoodsNH()
        {
            cbGoodsNH.DataSource = GoodsDAO.Instance.GetGoodsList();
            cbGoodsNH.DisplayMember = "NameGoods";
        }
        void LoadPriceGoodsNH(string name)
        {
            cbPriceNH.DataSource = GoodsDAO.Instance.SearchGoodsByName(name);
            cbPriceNH.DisplayMember = "PriceIn";
            cbPriceNH.Text = decimal.Parse(cbPriceNH.Text.Replace(",", ".")).ToString("0,0.##");
        }
        private void cbGoodsNH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = "";
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Goods selected = cb.SelectedItem as Goods;
            name = selected.NameGoods;
            LoadPriceGoodsNH(name);
            soluongNH.Value = 0;
            TotalNH();
        }
        void TotalNH()
        {
            totalNH = 0;
            int sl = (int)soluongNH.Value;
            int price = (cbPriceNH.SelectedItem as Goods).PriceIn;
            totalNH = price * sl;
            textTotalNH.Text = totalNH.ToString();
            textTotalNH.Text = decimal.Parse(textTotalNH.Text.Replace(",", ".")).ToString("0,0.##");
            if (textTotalNH.Text == "00")
            {
                textTotalNH.Text = "0";
            }
        }
        private void soluongNH_ValueChanged(object sender, EventArgs e)
        {
            TotalNH();
        }
        private void btnPayNH_Click(object sender, EventArgs e)
        {
            string type = "Nhập hàng";
            string name = (cbGoodsNH.SelectedItem as Goods).NameGoods;
            int price = (cbPriceNH.SelectedItem as Goods).PriceIn;
            int count = (int)soluongNH.Value;
            int total = totalNH;
            string day = dayNH.Value.ToString("yyyMMdd");
            if (TotalExpenditureDAO.Instance.InsertExpenditure(type, name, price, count, total, day))
            {
                MessageBox.Show("Nhập hàng thành công");
                LoadListExpenditure();
                if (GoodsDAO.Instance.UpdateCountGoodsNH(name,count))
                {
                    LoadListHH();
                }
                else
                {
                    MessageBox.Show("Lỗi");
                }
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
            if (TotalExpenditureDAO.Instance.RefreshExpenditure())
            {
                LoadListExpenditure();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }    
        }
        #endregion
        #region Hang hoa
        void LoadListHH()
        {
            listHH.DataSource = GoodsDAO.Instance.GetGoodsList();
            dtgvHH.Columns["IdGoods"].HeaderText = "ID";
            dtgvHH.Columns["NameGoods"].HeaderText = "Tên sản phẩm";
            dtgvHH.Columns["TypeGoods"].HeaderText = "Phân loại";
            dtgvHH.Columns["PriceIn"].HeaderText = "Giá mua(VNĐ)";
            dtgvHH.Columns["PriceOut"].HeaderText = "Giá bán(VNĐ)";
            dtgvHH.Columns["ExpGoods"].HeaderText = "NSX";
            dtgvHH.Columns["ExpGoods"].DefaultCellStyle.Format = "dd/MM/yyy";
            dtgvHH.Columns["MfgGoods"].HeaderText = "HSD";
            dtgvHH.Columns["MfgGoods"].DefaultCellStyle.Format = "dd/MM/yyy";
            dtgvHH.Columns["QuantityGoods"].HeaderText = "Số lượng";
            dtgvHH.Columns["NameSupplier"].HeaderText = "Nhà cung cấp";
            dtgvHH.Columns["VAT"].HeaderText = "VAT(%)";

        }
        void AddHHBinding()
        {
            textIDHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "IdGoods", true, DataSourceUpdateMode.Never));
            textNameHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "NameGoods", true, DataSourceUpdateMode.Never));
            cbTypeHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "TypeGoods", true, DataSourceUpdateMode.Never));
            textPriceBuyHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "PriceIn", true, DataSourceUpdateMode.Never));
            textPriceSellHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "PriceOut", true, DataSourceUpdateMode.Never));
            NSX.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "ExpGoods", true, DataSourceUpdateMode.Never));
            HSD.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "MfgGoods", true, DataSourceUpdateMode.Never));
            textquantityHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "QuantityGoods", true, DataSourceUpdateMode.Never));
            cbNameSupplierHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "NameSupplier", true, DataSourceUpdateMode.Never));
            textVATHH.DataBindings.Add(new Binding("Text", dtgvHH.DataSource, "VAT", true, DataSourceUpdateMode.Never));
        }
        void LoadTypeGoods(ComboBox cb)
        {
            cb.DataSource = TypeGoodsDAO.Instance.GetTypeGoodsList();
            cb.DisplayMember = "Typegoods";
        }
        private void btnRefreshHH_Click(object sender, EventArgs e)
        {
            LoadListHH();
        }
        private void btnAddHH_Click(object sender, EventArgs e)
        {
            string name = textNameHH.Text;
            string type = cbTypeHH.Text;
            int priceIn = Convert.ToInt32(textPriceBuyHH.Text);
            int priceOut = Convert.ToInt32(textPriceSellHH.Text);
            string exp = NSX.Value.ToString("yyyMMdd");
            string mfg = HSD.Value.ToString("yyyMMdd");
            int quantity = Convert.ToInt32(textquantityHH.Text);
            string namesupplier = cbNameSupplierHH.Text;
            if (GoodsDAO.Instance.InsertGoods(name, type, priceIn, priceOut, exp, mfg, quantity, namesupplier))
            {
                MessageBox.Show("Thêm thành công");
                LoadListHH();
                LoadTypeGoodsBill(cbTypeBill);
                LoadNameGoodsNH();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnUpdateHH_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDHH.Text);
            string name = textNameHH.Text;
            string type = cbTypeHH.Text;
            int priceIn = Convert.ToInt32(textPriceBuyHH.Text);
            int priceOut = Convert.ToInt32(textPriceSellHH.Text);
            string exp = NSX.Value.ToString("yyyMMdd");
            string mfg = HSD.Value.ToString("yyyMMdd");
            int quantity = Convert.ToInt32(textquantityHH.Text);
            string namesupplier = cbNameSupplierHH.Text;
            if (GoodsDAO.Instance.UpdateGoods(id, name, type, priceIn, priceOut, exp, mfg, quantity, namesupplier))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadListHH();
                LoadTypeGoodsBill(cbTypeBill);
                LoadNameGoodsNH();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnDeleteHH_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textIDHH.Text);
            if (GoodsDAO.Instance.DeleteGoods(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListHH();
                LoadTypeGoodsBill(cbTypeBill);
                LoadNameGoodsNH();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void btnSearchTypeHH_Click(object sender, EventArgs e)
        {
            listHH.DataSource = GoodsDAO.Instance.SearchGoodsByType(cbSearchTypeHH.Text);
        }
        private void btnSearchNameHH_Click(object sender, EventArgs e)
        {
            listHH.DataSource = GoodsDAO.Instance.DeleteGoodsByNameSupplier(textSearchNameHH.Text);
        }
        #endregion
        #region Hoa don
        void LoadListBill()
        {
            listBill.DataSource = BillInDAO.Instance.GetBillList();
            dtgvBill.Columns["Id"].Visible = false;
            dtgvBill.Columns["NameGoods"].HeaderText = "Tên sản phẩm";
            dtgvBill.Columns["NameGoods"].Width = 290;
            dtgvBill.Columns["PriceOut"].HeaderText = "Giá";
            dtgvBill.Columns["PriceOut"].Width = 70;
            dtgvBill.Columns["CountGoods"].HeaderText = "Số lượng";
            dtgvBill.Columns["CountGoods"].Width = 80;
            dtgvBill.Columns["Total"].HeaderText = "Thành tiền";
            dtgvBill.Columns["Total"].Width = 100;
            dtgvBill.Columns["DateBill"].HeaderText = "Ngày mua";
            dtgvBill.Columns["DateBill"].Width = 100;
            dtgvBill.Columns["DateBill"].DefaultCellStyle.Format = "dd/MM/yyy";
        }
        void Total()
        {
            total = 0;
            int n = dtgvBill.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                total += int.Parse(dtgvBill.Rows[i].Cells["Total"].Value.ToString());
            }
            textTotalBill.Text = total.ToString();
            textTotalBill.Text = decimal.Parse(textTotalBill.Text.Replace(",", ".")).ToString("0,0.##");
            if (textTotalBill.Text == "00")
            {
                textTotalBill.Text = "0";
            }    
        }
        void Discount()
        {
            discount = 0;
            discount = total * DiscountbyRank(rank)/100;
            textDiscount.Text = discount.ToString();
            textDiscount.Text = decimal.Parse(textDiscount.Text.Replace(",", ".")).ToString("0,0.##");
            if (textDiscount.Text == "00")
            {
                textDiscount.Text = "0";
            }
        }
        void Pay()
        {
            textPay.Text = (total - discount).ToString();
            textPay.Text = decimal.Parse(textPay.Text.Replace(",", ".")).ToString("0,0.##");
            if (textPay.Text == "00")
            {
                textPay.Text = "0";
            }
        }
        void LoadTypeGoodsBill(ComboBox cb)
        {
            cb.DataSource = TypeGoodsDAO.Instance.GetTypeGoodsBillList();
            cb.DisplayMember = "Typegoods";
        }
        void LoadGoodsByType(string type)
        {
            cbGoodsBill.DataSource = GoodsDAO.Instance.SearchGoodsByType(type); 
            cbGoodsBill.DisplayMember = "nameGoods";
            if (cbGoodsBill.Items.Count == 0)
            {
                cbGoodsBill.Text = "";
            }    
        }
        void LoadPriceByGoods(string name)
        {
            cbPriceBill.DataSource = GoodsDAO.Instance.SearchGoodsByName(name);
            cbPriceBill.DisplayMember = "priceOut";
            cbPriceBill.Text = decimal.Parse(cbPriceBill.Text.Replace(",", ".")).ToString("0,0.##");
        }
        private void cbTypeBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = "";
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            TypeGoods selected = cb.SelectedItem as TypeGoods;
            type = selected.Typegoods;
            LoadGoodsByType(type);
        }
        private void cbGoodsBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = "";
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Goods selected = cb.SelectedItem as Goods;
            name = selected.NameGoods;
            LoadPriceByGoods(name);
        }
        void LoadCustomerBill(ComboBox cb)
        {
            cb.DataSource = CustomerDAO.Instance.GetKHList();
            cb.DisplayMember = "PhoneNumCustomer";
        }
        void LoadNameCustomerByPhone(string phone)
        {
            cbNameKHBill.DataSource = CustomerDAO.Instance.SearchCustomerByPhone(phone);
            cbNameKHBill.DisplayMember = "nameCustomer";
        }
        string GetRankByPhone(string phone)
        {
            int n = dtgvKH.Rows.Count;
            string rank = "";
            for (int i = 0; i < n; i++)
            {
                if (phone == dtgvKH.Rows[i].Cells["PhoneNumCustomer"].Value.ToString())
                {
                    rank = dtgvKH.Rows[i].Cells["Rank"].Value.ToString();
                }
            }
            return rank;
        }
        private void cbSDTKHBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string phone = "";
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Customer selected = cb.SelectedItem as Customer;
            phone = selected.PhoneNumCustomer;
            LoadNameCustomerByPhone(phone);
            rank = GetRankByPhone(phone);
            Discount();
            Pay();
        }
        private void cbSDTKHBill_TextChanged(object sender, EventArgs e)
        {
            Discount();
            Pay();
            cbNameKHBill.Text = null;
        }
        private void cbSDTKHBill_TextUpdate(object sender, EventArgs e)
        {
            Discount();
            Pay();
            cbNameKHBill.Text = null;
        }
        int DiscountbyRank(string rank)
        {
            if (rank == "Không")
                return 0;
            else if (rank == "Đồng")
                return 2;
            else if (rank == "Bạc")
                return 5;
            else if (rank == "Vàng")
                return 10;
            else if (rank == "Kim Cương")
                return 20;
            return 0;
        }
        private void btnAddBill_Click(object sender, EventArgs e)
        {
            string nameGoods = (cbGoodsBill.SelectedItem as Goods).NameGoods;
            int price = (cbPriceBill.SelectedItem as Goods).PriceOut;
            int count = (int)soluongBill.Value;
            int total = price * count;
            string date = dayBill.Value.ToString("yyyMMdd");
            bool check = false;
            int countcurrent = 0;
            int quantityHH = 0;
            int n = dtgvBill.Rows.Count;
            for (int i=0; i < n; i++)
            {
                if (nameGoods == dtgvBill.Rows[i].Cells["NameGoods"].Value.ToString())
                {
                    check = true;
                    countcurrent = int.Parse(dtgvBill.Rows[i].Cells["CountGoods"].Value.ToString());
                    break;
                }    
            }
            n = dtgvHH.Rows.Count;
            for (int i=0; i<n; i++)
            {
                if (nameGoods == dtgvHH.Rows[i].Cells["NameGoods"].Value.ToString())
                {
                    quantityHH = int.Parse(dtgvHH.Rows[i].Cells["QuantityGoods"].Value.ToString());
                    break;
                }
            }
            if (check)
            {
                if ((quantityHH - count) >= 0)
                {
                    if (countcurrent + count == 0)
                    {
                        if (BillInDAO.Instance.DeleteBill(nameGoods))
                        {
                            LoadListBill();
                            Total();
                            if (GoodsDAO.Instance.UpdateCountGoods(nameGoods, count))
                            {
                                LoadListHH();
                            }
                            else
                            {
                                MessageBox.Show("Lỗi thêm sản phẩm");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lỗi thêm sản phẩm");
                        }
                    }
                    else if (countcurrent + count > 0)
                    {
                        if (BillInDAO.Instance.MergeBill(nameGoods, count, total))
                        {
                            LoadListBill();
                            Total();
                            if (GoodsDAO.Instance.UpdateCountGoods(nameGoods, count))
                            {
                                LoadListHH();
                            }
                            else
                            {
                                MessageBox.Show("Lỗi thêm sản phẩm");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lỗi thêm sản phẩm");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi thêm sản phẩm");
                    }
                }
                else
                {
                    if (quantityHH == 0)
                    {
                        MessageBox.Show("Sản phẩm này đã hết hàng");
                    }
                    else
                    {
                        MessageBox.Show("Sản phẩm này chỉ còn " + (quantityHH).ToString() + " sản phẩm");
                    }
                }    
            }
            else
            {
                if ((quantityHH - count) >= 0)
                {
                    if (BillInDAO.Instance.InsertBill(nameGoods, price, count, total, date))
                    {
                        LoadListBill();
                        Total();
                        if (GoodsDAO.Instance.UpdateCountGoods(nameGoods, count))
                        {
                            LoadListHH();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi thêm sản phẩm");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi thêm sản phẩm");
                    }
                }
                else
                {
                    if (quantityHH == 0)
                    {
                        MessageBox.Show("Sản phẩm này đã hết hàng");
                    }   
                    else
                    {
                        MessageBox.Show("Sản phẩm này chỉ còn " + (quantityHH).ToString() + " sản phẩm");
                    }    
                }
            }    
        }
        private void btnThanhtoanBill_Click(object sender, EventArgs e)
        {
            string name = cbNameKHBill.Text;
            string phone = cbSDTKHBill.Text;
            string add = "";
            string birth = "";
            int points = total;
            string rank = RankByPoints(points);
            bool check = false;
            int n = dtgvKH.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                if (phone == dtgvKH.Rows[i].Cells["PhoneNumCustomer"].Value.ToString())
                {
                    add = dtgvKH.Rows[i].Cells["AddressCustomer"].Value.ToString();
                    birth = String.Format("{0:yyyyMMdd}", dtgvKH.Rows[i].Cells["BirthDayCustomer"].Value);
                    points = points + (int)dtgvKH.Rows[i].Cells["AccumulatedPoints"].Value;
                    rank = RankByPoints(points);
                    check = true;
                    break;
                }
            }
            if (check)
            {
                if (CustomerDAO.Instance.UpdateBillCustomer(name,add,phone,birth,points,rank))
                {
                    LoadListKH();
                    cbSDTKHBill.Text = null;
                    cbNameKHBill.Text = null;
                    textDiscount.Text = null;
                    textPay.Text = null;
                    Total();
                }    
                else
                {
                    MessageBox.Show("Lỗi thanh toán không thành công");
                }    
            }    
            else
            {
                if (CustomerDAO.Instance.InsertCustomer(name,add,phone,birth,points,rank))
                {
                    LoadListKH();
                    cbSDTKHBill.Text = null;
                    cbNameKHBill.Text = null;
                    textDiscount.Text = null;
                    textPay.Text = null;
                    Total();
                }    
                else
                {
                    MessageBox.Show("Lỗi thanh toán không thành công");
                }    
            }
            if (TotalRevenueDAO.Instance.PayRevenue())
            {
                MessageBox.Show("Thanh toán thành công");
                LoadListRevenue();
                LoadListBill();
                Total();
            }
            else
            {
                MessageBox.Show("Lỗi thanh toán không thành công");
            }
            if (TotalRevenueDAO.Instance.RefreshRevenue())
            {
                LoadListRevenue();
                LoadListBill();
                Total();
            }   
            else
            {
                MessageBox.Show("Lỗi thanh toán không thành công");
            }
            textTotalBill.Text = "0";
        }
        #endregion
        #region Doanh thu 
        //
        // Tong thu
        //
        void LoadListRevenue()
        {
            listRevenue.DataSource = TotalRevenueDAO.Instance.GetTotalRevenueList();
            dtgvRevenue.Columns["Id"].Visible = false;
            dtgvRevenue.Columns["NameGoods"].HeaderText = "Tên sản phẩm";
            dtgvRevenue.Columns["NameGoods"].Width = 250;
            dtgvRevenue.Columns["PriceOut"].HeaderText = "Giá";
            dtgvRevenue.Columns["PriceOut"].Width = 70;
            dtgvRevenue.Columns["CountGoods"].HeaderText = "Số lượng";
            dtgvRevenue.Columns["CountGoods"].Width = 80;
            dtgvRevenue.Columns["Total"].HeaderText = "Thành tiền";
            dtgvRevenue.Columns["Total"].Width = 100;
            dtgvRevenue.Columns["DateBill"].HeaderText = "Ngày mua";
            dtgvRevenue.Columns["DateBill"].Width = 100;
            dtgvRevenue.Columns["DateBill"].DefaultCellStyle.Format = "dd/MM/yyy";
            TotalRevenue();
        }
        private void btnSearchRevenue_Click(object sender, EventArgs e)
        {
            string daystart = dayStartRevenue.Value.ToString("yyyMMdd");
            string dayfinish = dayFinishRevenue.Value.ToString("yyyMMdd");
            if (dayStartRevenue.Value <= dayFinishRevenue.Value)
            {
                listRevenue.DataSource = TotalRevenueDAO.Instance.GetRevenueByDate(daystart, dayfinish);
                TotalRevenue();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lại mốc thời gian");
            }
        }
        void TotalRevenue()
        {
            totalRevenue = 0;
            int n = dtgvRevenue.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                totalRevenue += int.Parse(dtgvRevenue.Rows[i].Cells["Total"].Value.ToString());
            }
            textTotalRevenueDay.Text = totalRevenue.ToString();
            textTotalRevenueDay.Text = decimal.Parse(textTotalRevenueDay.Text.Replace(",", ".")).ToString("0,0.##");
            if (textTotalRevenueDay.Text == "00")
            {
                textTotalRevenueDay.Text = "0";
            }
        }
        //
        // Tong chi
        //
        void LoadListExpenditure()
        {
            listExpenditure.DataSource = TotalExpenditureDAO.Instance.GetTotalExpenditureList();
            dtgvExpenditure.Columns["Id"].Visible = false;
            dtgvExpenditure.Columns["TypeBill"].HeaderText = "Hình thức";
            dtgvExpenditure.Columns["NameGoods"].HeaderText = "Tên sản phẩm";
            dtgvExpenditure.Columns["PriceOut"].HeaderText = "Đơn giá";
            dtgvExpenditure.Columns["CountGoods"].HeaderText = "Số lượng";
            dtgvExpenditure.Columns["Total"].HeaderText = "Thành tiền";
            dtgvExpenditure.Columns["DateBill"].HeaderText = "Ngày mua";
            dtgvExpenditure.Columns["DateBill"].DefaultCellStyle.Format = "dd/MM/yyy";
            TotalExpenditure();
        }
        private void btnSearchExpenditure_Click(object sender, EventArgs e)
        {
            string daystart = dayStartExpenditure.Value.ToString("yyyMMdd");
            string dayfinish = dayFinishExpenditure.Value.ToString("yyyMMdd");
            if (dayStartExpenditure.Value <= dayStartExpenditure.Value)
            {
                listExpenditure.DataSource = TotalExpenditureDAO.Instance.GetExpenditureByDate(daystart, dayfinish);
                TotalExpenditure();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn lại mốc thời gian");
            }
        }
        void TotalExpenditure()
        {
            totalExpenditure = 0;
            int n = dtgvExpenditure.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                totalExpenditure += int.Parse(dtgvExpenditure.Rows[i].Cells["Total"].Value.ToString());
            }
            textTotalExpenditureDay.Text = totalExpenditure.ToString();
            textTotalExpenditureDay.Text = decimal.Parse(textTotalExpenditureDay.Text.Replace(",", ".")).ToString("0,0.##");
            if (textTotalExpenditureDay.Text == "00")
            {
                textTotalExpenditureDay.Text = "0";
            }
        }


        #endregion


    }
}
