using Dapper;
using DapperProject.Dtos.CategoryDtos;
using DapperProject.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DapperProject
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-TMVO8N4\\SQLEXPRESS;Initial Catalog=DapperProjectVt;Integrated Security=True;MultipleActiveResultSets=True;");

        async Task ListProduct()
        {
            string query = "select * from Tbl_Product";
            using (var connection = new SqlConnection("Data Source=DESKTOP-TMVO8N4\\SQLEXPRESS;Initial Catalog=DapperProjectVt;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                dataGridView1.DataSource = values;
            }
        }
        async Task CategoryList()
        {
            string query = "select * from Tbl_Category";
            using (var connection = new SqlConnection("Data Source=DESKTOP-TMVO8N4\\SQLEXPRESS;Initial Catalog=DapperProjectVt;Integrated Security=True; MultipleActiveResultSets=True;"))
            {
                var values = await connection.QueryAsync<ResultCategoryDto>(query);
                cmbList.DataSource = values.ToList();
                cmbList.DisplayMember = "CategoryName";
                cmbList.ValueMember = "CategoryID";
            }
        }
        async void InsertProduct()
        {
            string query = "insert into Tbl_Product (ProductName,UnitPrice, TotalPrice, ProductStock, CategoryID) values (@p1,@p2,@p3,@p4,@p5)";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", txtName.Text);
            parameters.Add("@p2", txtUnitPrice.Text);
            parameters.Add("@p3", txtToalPrice.Text);
            parameters.Add("@p4", txtStock.Text);
            parameters.Add("@p5", int.Parse(cmbList.SelectedValue.ToString()));
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Ekleme tamamlandı", "Ekleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        async void UpdateProduct()
        {
            string query = "update Tbl_Product set ProductName=@p1,UnitPrice=@p2, TotalPrice=@p3, ProductStock=@p4, CategoryID=@p5 where ProductID=@p6 ";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", txtName.Text);
            parameters.Add("@p2", txtUnitPrice.Text);
            parameters.Add("@p3", txtToalPrice.Text);
            parameters.Add("@p4", txtStock.Text);
            parameters.Add("@p5", int.Parse(cmbList.SelectedValue.ToString()));
            parameters.Add("@p6", txtid.Text);
            await connection.ExecuteAsync(query, parameters);
            MessageBox.Show("Güncelleme tamamlandı", "Güncelleme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        async Task DeleteProduct()
        {
            string query = "Delete from Tbl_Product where ProductID=@p1";
            using (var connection = new SqlConnection("Data Source=DESKTOP-TMVO8N4\\SQLEXPRESS;Initial Catalog=DapperProjectVt;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@p1", txtid.Text);
                await connection.ExecuteAsync(query, parameters);
            }
            MessageBox.Show("Silme tamamlandı", "Silme İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        async void SearchProduct()
        {
            string query = "select * from Tbl_Product where ProductName=@p1";
            var parameters = new DynamicParameters();
            parameters.Add("@p1", txtName.Text);
            var values = await connection.QueryAsync<ResultProductDto>(query);
            dataGridView1.DataSource = values;
            MessageBox.Show("Arama tamamlandı", "Arama İşlemi Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void Clear()
        {
            txtid.Text = "";
            txtName.Text = "";
            txtUnitPrice.Text = "";
            txtToalPrice.Text = "";
            txtStock.Text = "";
            cmbList.Text = "";
        }
        private async void btnList_Click(object sender, EventArgs e)
        {
            await ListProduct();
        }
        private async void FrmProduct_Load(object sender, EventArgs e)
        {
            await ListProduct();
            await CategoryList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtUnitPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtToalPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtStock.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbList.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }
        private async void btnInsert_Click(object sender, EventArgs e)
        {
            InsertProduct();
            await ListProduct();
            Clear();
        }
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProduct();
            await ListProduct();
            await CategoryList();
            Clear();
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await DeleteProduct();
            Clear();
            await ListProduct();
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            SearchProduct();
            Clear();
            await ListProduct();
            await CategoryList();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
