using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DbHelper db = new DbHelper();

  

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // მონაცემების ჩატვირთვა
        void LoadData()
        {
            dataGridView1.DataSource = db.GetAll();
        }

        // Load ღილაკი
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // დამატება
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var student = new Student
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Age = int.Parse(txtAge.Text)
                };

                db.Add(student);
                LoadData();
                MessageBox.Show("დამატებულია!");
            }
            catch
            {
                MessageBox.Show("შეცდომა!");
            }
        }

        // განახლება
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var student = new Student
                {
                    Id = int.Parse(txtId.Text),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Age = int.Parse(txtAge.Text)
                };

                db.Update(student);
                LoadData();
                MessageBox.Show("განახლდა!");
            }
            catch
            {
                MessageBox.Show("შეცდომა!");
            }
        }

        // წაშლა
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);

                db.Delete(id);
                LoadData();
                MessageBox.Show("წაიშალა!");
            }
            catch
            {
                MessageBox.Show("შეცდომა!");
            }
        }

        // DataGrid-ზე კლიკი → TextBox-ებში ჩასმა
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtId.Text = row.Cells["Id"].Value.ToString();
                txtFirstName.Text = row.Cells["FirstName"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtAge.Text = row.Cells["Age"].Value.ToString();
            }
        }
    }
}