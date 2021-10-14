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

namespace CRUD_Operation
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GALAGKM\SQLEXPRESS01;Initial Catalog=USER_TABLE;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            if (I())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO member(ID,Datums,Objekts,DarbaStundas) VALUES (@ID,@Datums,@Objekts,@DarbaStundas)", con);
                cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Datums", textBox2.Text);
                cmd.Parameters.AddWithValue("@Objekts", textBox3.Text);
                cmd.Parameters.AddWithValue("@DarbaStundas", textBox4.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                BindData();
                dataGridView1.Refresh();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox1.Focus();

                MessageBox.Show("Data saved", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Refresh();
            }
        }
        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from member", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private bool I()
        {
            if(textBox1.Text == string.Empty)
            {
                MessageBox.Show("some of data fields are empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (u())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE member SET Datums=@Datums, Objekts=@Objekts, DarbaStundas=@DarbaStundas WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Datums", textBox2.Text);
                cmd.Parameters.AddWithValue("@Objekts", textBox3.Text);
                cmd.Parameters.AddWithValue("@DarbaStundas", textBox4.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                BindData();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                MessageBox.Show("Data updated");
            }
        }

        private bool u()
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("some of data fields are empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (D())
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GALAGKM\SQLEXPRESS01;Initial Catalog=USER_TABLE;Integrated Security=True");
                con.Open();

                if (MessageBox.Show("are you sur want to delete?", "Data deleted", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("DELETE member WHERE ID=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    BindData();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";

                    MessageBox.Show("Data deleted");
                }
            }
        }

        private bool D()
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please input ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.memberTableAdapter.Fill(this.uSER_TABLEDataSet1.member);

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GALAGKM\SQLEXPRESS01;Initial Catalog=USER_TABLE;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM member", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (o())
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GALAGKM\SQLEXPRESS01;Initial Catalog=USER_TABLE;Integrated Security=True");
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM member WHERE Objekts=@Objekts", con);

                cmd.Parameters.AddWithValue("@Objekts", textBox3.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private bool o()
        {
            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Please input Objekts field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        } 
    }
}
