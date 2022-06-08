using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KhachSan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cn;
        SqlCommand cmd,cmd2;
        string sql= @"Data Source=H114\SQLEXPRESS;Initial Catalog=Pratice;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "Select ROW_NUMBER() Over (Order by TenKH) as [Stt], TenKH,GioiTinh,LoaiPhong,SLPhong from dbo.KhachSan";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(sql);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "Select ROW_NUMBER() Over (Order by TenKH) as [Stt], TenKH,GioiTinh,LoaiPhong,SLPhong from dbo.KhachSan";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(sql);
            cn.Open();
            cmd=cn.CreateCommand();
            int a = int.Parse(txtQuanity.Text.ToString());
            string gt = "";
            if (rdbNam.Checked)
            {
                gt = "Nam";
            }
            if(rdbNu.Checked)
            {
                gt = "Nữ";
            }    
            cmd.CommandText = "Insert into KhachSan values(N'"+txtName.Text+"',N'"+gt+"',N'"+cbType.Text+"','"+a+"')";
            cmd.ExecuteNonQuery();
            loaddata();
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }
            rdbNam.Checked = false;
            rdbNu.Checked = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(sql);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "Delete from KhachSan where TenKH=N'" + txtName.Text + "'";
            cmd.ExecuteNonQuery();
           
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(sql);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "Select ROW_NUMBER() Over (Order by TenKH) as [Stt], TenKH,GioiTinh,LoaiPhong,SLPhong from dbo.KhachSan where TenKH=N'" + txtSearch.Text+"' or LoaiPhong=N'"+txtSearch.Text+"' or GioiTinh=N'"+txtSearch.Text+"'";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            txtName.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            cbType.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtQuanity.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            string gt = dataGridView1.Rows[i].Cells[2].Value.ToString();
            if(gt == "Nam")
            {
                rdbNam.Checked = true;
            }
            if (gt == "Nữ")
            {
                rdbNam.Checked = true;
            }

        }
    }
}
