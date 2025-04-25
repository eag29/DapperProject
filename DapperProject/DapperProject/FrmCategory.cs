using Dapper;
using DapperProject.Dtos.CategoryDtos;
using DapperProject.Dtos.CategoryDtos;
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
using System.Xml.Linq;

namespace DapperProject
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-TMVO8N4\\SQLEXPRESS;Initial Catalog=DapperProjectVt;Integrated Security=True;MultipleActiveResultSets=True;");

        async Task ListCategory()
        {
            string query = "select * from Tbl_Category";
            using (var connection = new SqlConnection("Data Source=DESKTOP-TMVO8N4\\SQLEXPRESS;Initial Catalog=DapperProjectVt;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                dataGridView1.DataSource = values;
            }
        }
        async void InsertCategory()
        {
            string query = "insert into Tbl_Category (CategoryName) values (@p1)";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", txtName.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Ekleme tamamlandı", "Ekleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        async void UpdateCategory()
        {
            string query = "update Tbl_Category set CategoryName=@p1 where CategoryID=@p2 ";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", txtName.Text);
            parameters.Add("@p2", txtid.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Güncelleme tamamlandı", "Güncelleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        async Task DeleteCategory()
        {
            string query = "Delete from Tbl_Category where CategoryID=@p1";
            using (var connection = new SqlConnection("Data Source=DESKTOP-TMVO8N4\\SQLEXPRESS;Initial Catalog=DapperProjectVt;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p1", txtid.Text);
                await connection.ExecuteAsync(query, parameters);
            }
            MessageBox.Show("Silme tamamlandı", "Silme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        async void SearchCategory()
        {
            string query = "select * from Tbl_Category where CategoryName=@p1";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", txtName.Text);
            var values = await connection.QueryAsync<ResultCategoryDto>(query);
            dataGridView1.DataSource = values;
            MessageBox.Show("Arama tamamlandı", "Arama İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void Clear()
        {
            txtid.Text = "";
            txtName.Text = "";
        }
        private async void btnInsert_Click(object sender, EventArgs e)
        {
            InsertCategory();
            await ListCategory();
            Clear();
        }
        private async void btnList_Click_1(object sender, EventArgs e)
        {
            await ListCategory();
        }
        private async void FrmCategory_Load_1(object sender, EventArgs e)
        {
            await ListCategory();
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        private async void btnUpdate_Click_1(object sender, EventArgs e)
        {
            UpdateCategory();
            await ListCategory();
            Clear();
        }
        private async void btnDelete_Click_1(object sender, EventArgs e)
        {
            await DeleteCategory();
            Clear();
            await ListCategory();
        }
        private async void btnSearch_Click_1(object sender, EventArgs e)
        {
            SearchCategory();
            Clear();
            await ListCategory();
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            Clear();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
