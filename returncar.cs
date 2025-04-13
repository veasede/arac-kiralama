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

namespace carrental
{
    public partial class returncar : Form
    {
        public returncar()
        {
            InitializeComponent();
            returncarload();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-RPNNDSK\\SQLEXPRESS; Initial Catalog=carrental; Integrated Security=True;");
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataReader dr;
        string proid;
        string sql;
        string sql1;
        bool Mode = true;
        string id;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                cmd = new SqlCommand("select car_id,cust_id,date,due,DATEDIFF(dd,due,GETDATE()) as elap from rental where car_id='"+txtcarid.Text+"'",con);
                con.Open();
                dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    txtcustid.Text = dr["cust_id"].ToString();
                    txtdate.Text = dr["due"].ToString();

                    string elap = dr["elap"].ToString();

                    int elapped = int.Parse(elap);
                    txtelp.Text = elap;
                    con.Close();

                    if (elapped>0)
                    {
                        

                        int fine = elapped * 100;

                        txtfine.Text = fine.ToString();
                        con.Close();

                    }

                    else
                    {
                        txtfine.Text = "0";
                        //txtfine.Text = "0";
                        con.Close();
                    }
                    con.Close();
                }
                con.Close();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string carid = txtcarid.Text;
            string custid = txtcustid.Text;
            string date = txtdate.Text;
            string elp = txtelp.Text;
            string fine = txtfine.Text;





                sql = "insert into returncar(car_id, cust_id,date,elp,fine) values(@car_id, @cust_id,@date,@elp,@fine)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@car_id", carid);
                cmd.Parameters.AddWithValue("@cust_id", custid);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@elp", elp);
                cmd.Parameters.AddWithValue("@fine", fine);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record added");
                con.Close();

            }

        public void returncarload()
        {
            sql = "select * from returncar";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
            }
            con.Close();


        }




        public void getid(string id)

        {
            sql = "select * from returncar where cust_id='" + id + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtcarid.Text = dr[1].ToString();
                txtcustid.Text = dr[2].ToString();
                txtdate.Text = dr[3].ToString();
                txtelp.Text = dr[4].ToString();
                txtfine.Text = dr[5].ToString();

            }
            con.Close();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {

                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from returncar where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted");
                con.Close();

            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void returncar_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.returncarload();
        }
    }
}
