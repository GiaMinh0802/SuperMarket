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
        public fMain()
        {
            InitializeComponent();
            LoadAll();
        }
        #region Method
        void LoadAll()
        {
            dtgvKH.DataSource = listKH;
            LoadListKH();
            AddKHBinding();
            LoadListNV();
        }
        //
        // Khach hang
        //
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
        private void btnrefreshKH_Click(object sender, EventArgs e)
        {
            LoadListKH();
        }
        void LoadRank(ComboBox cb)
        {
            cb.DataSource = RankCustomerDAO.Instance.GetRankList();
            cb.DisplayMember = "Rank";
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
        //
        // Nhan vien
        //
        void LoadListNV()
        {

        }
        //
        // 
        //
        #endregion

        #region Events
        //
        // Khach hang
        //
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
                MessageBox.Show("Sửa thành công");
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
        //
        //
        //
        #endregion
    }
}
