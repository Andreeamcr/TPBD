using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Angajati
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        OracleCommandBuilder cmd;
        OracleCommand command;
        OracleParameter p1, p2, p3, p4, p5, p6, p7, p8;
        //BindingSource bs1 = new BindingSource();
        string strSQL;
        OracleDataReader dr;

        private void reloadGrid()
        {
            strSQL = "Select * from ANGAJATI ORDER BY ID";
            da = new OracleDataAdapter(strSQL, conn);
            ds = new DataSet();
            da.Fill(ds, "angajati");
            dataGridView1.DataSource = ds.Tables["angajati"].DefaultView;
            dataGridView2.DataSource = ds.Tables["angajati"].DefaultView;
            dataGridView3.DataSource = ds.Tables["angajati"].DefaultView;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new OracleConnection("USER ID=STUDENT;PASSWORD=student;DATA SOURCE=localhost:1521/XE");
                strSQL = "SELECT * FROM ANGAJATI ORDER BY ID";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "angajati");
                cmd = new OracleCommandBuilder(da);
                dataGridView1.DataSource = ds.Tables["angajati"].DefaultView;
                dataGridView2.DataSource = ds.Tables["angajati"].DefaultView;
                dataGridView3.DataSource = ds.Tables["angajati"].DefaultView;
                
                
            }
            catch(OracleException ex)
            {
                label1.Text = "EROARE: " + ex.Message.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //var form = new Form2();
            //form.Show();
            //will be useful for showing another form in calculation tab
            try
            {
                conn.Open();
                
                var id = dataGridView1.Rows.Cast<DataGridViewRow>().Max(r => Convert.ToInt32(r.Cells["ID"].Value));

                p1 = new OracleParameter();
                p2 = new OracleParameter();
                p3 = new OracleParameter();
                p4 = new OracleParameter();
                p5 = new OracleParameter();
                p6 = new OracleParameter();
                p7 = new OracleParameter();
                p8 = new OracleParameter();

                p1.Value = ++id;
                p2.Value = textBox9.Text;
                p3.Value = textBox3.Text;
                p4.Value = textBox4.Text;
                p5.Value = textBox5.Text;
                p6.Value = textBox6.Text;
                p7.Value = textBox7.Text;
                p8.Value = textBox8.Text;

                strSQL = "INSERT INTO ANGAJATI VALUES (:1, :2, :3, :4, :5, :6, :7, :8)";
                command = new OracleCommand(strSQL, conn);
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                command.Parameters.Add(p3);
                command.Parameters.Add(p4);
                command.Parameters.Add(p5);
                command.Parameters.Add(p6);
                command.Parameters.Add(p7);
                command.Parameters.Add(p8);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("S-a adaugat cu succes!");
                reloadGrid();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Eroare la adaugare" + ex.ToString());
            }


        }

        private void textBox9_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //TODO: make sure if you should introduce int or float
        private void textBox5_keyPressed(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //var form2 = new Form2();
            //form2.Show();

            //TODO: de facut calculul cu parametrii extrasi din tabela si facut un reloadGrid la final 

            conn.Open();
            command = new OracleCommand("SELECT * FROM PROCENTE", conn);
            dr = command.ExecuteReader();
            richTextBox1.Text = "";
            dr.Read();
            richTextBox1.Text = richTextBox1.Text + " " + dr.GetDecimal(0) + " " + dr.GetDecimal(1) + " " + dr.GetDecimal(2) + "\n";
            dr.Close();
            conn.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //TODO: implementeaza cautare si afisare inainte de stergere folosind clauza WHERE
            dataGridView2.ReadOnly = false;
            DialogResult dialog = MessageBox.Show("Doriti stergere?", "Stergere", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                if(ds.Tables["angajati"].Rows.Count>0)
                {
                    int rownum = (dataGridView2.CurrentCell.RowIndex);
                    DataRow row = ds.Tables["angajati"].Rows[rownum];
                    row.Delete();
                    da.Update(ds.Tables["angajati"]);
                    dataGridView2.ReadOnly = true;

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void textBox6_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox8_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                da.Update(ds.Tables["angajati"]);
                ds.AcceptChanges();
                MessageBox.Show("Actualizat cu succes!");
                //dataGridView1.ReadOnly = false;
                //DialogResult dialog = MessageBox.Show("Actualizati?", "Actualizare", MessageBoxButtons.YesNoCancel);
                //if(dialog == DialogResult.Yes)
                //{
                //    int rownum = (dataGridView2.CurrentCell.RowIndex);
                //    DataRow row = ds.Tables["angajati"].Rows[rownum];
                //    row.AcceptChanges();
                //    da.Update(ds.Tables["angajati"]);
                //    dataGridView2.ReadOnly = true;
                //    MessageBox.Show("Actualizat cu succes!");
                //}
                //else if(dialog == DialogResult.Cancel)
                //{
                //    ds.RejectChanges();
                //}


            }
            catch(Exception ex)
            {
                MessageBox.Show("Eroare la actualizare " + ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ds.RejectChanges();
            dataGridView1.ReadOnly = false;
            
        }

       
    }
}
