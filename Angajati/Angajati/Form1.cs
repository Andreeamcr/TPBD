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
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["TOTAL_BRUT"].Visible = false;
            dataGridView1.Columns["BRUT_IMPOZITABIL"].Visible = false;
            dataGridView1.Columns["VIRAT_CARD"].Visible = false;

            dataGridView2.DataSource = ds.Tables["angajati"].DefaultView;
            dataGridView2.Columns["ID"].ReadOnly = true;
            dataGridView2.Columns["TOTAL_BRUT"].Visible = false;
            dataGridView2.Columns["BRUT_IMPOZITABIL"].Visible = false;
            dataGridView2.Columns["VIRAT_CARD"].Visible = false;

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
                dataGridView1.Columns["ID"].ReadOnly = true;
                dataGridView1.Columns["TOTAL_BRUT"].Visible = false;
                dataGridView1.Columns["BRUT_IMPOZITABIL"].Visible = false;
                dataGridView1.Columns["VIRAT_CARD"].Visible = false;

                dataGridView2.DataSource = ds.Tables["angajati"].DefaultView;
                dataGridView2.Columns["ID"].ReadOnly = true;
                dataGridView2.Columns["TOTAL_BRUT"].Visible = false;
                dataGridView2.Columns["BRUT_IMPOZITABIL"].Visible = false;
                dataGridView2.Columns["VIRAT_CARD"].Visible = false;

                dataGridView3.DataSource = ds.Tables["angajati"].DefaultView;
                dataGridView3.Columns["ID"].ReadOnly = true;

                
                
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
                OracleParameter p9 = new OracleParameter();
                OracleParameter p10 = new OracleParameter();
                OracleParameter p11 = new OracleParameter();

                p1.Value = ++id;
                p2.Value = textBox9.Text;
                p3.Value = textBox3.Text;
                p4.Value = textBox4.Text;
                p5.Value = textBox5.Text;
                p6.Value = textBox6.Text;
                p7.Value = textBox7.Text;
                p8.Value = textBox8.Text;

                double brut_imp = 0;
                double tot_brut = 0;
                double virat_card = 0;
                double salar_baza = 0;
                double premii_brute = 0;
                double retineri = 0;
                double spor = 0;
                double cas = 0;
                double cass = 0;
                double imp = 0;
                double CAS = 0;
                double CASS = 0;
                double IMP = 0;

                OracleCommand command1 = new OracleCommand("SELECT * FROM PROCENTE", conn);
                dr = command1.ExecuteReader();
                dr.Read();
                cas = dr.GetDouble(0);
                cass = dr.GetDouble(1);
                imp = dr.GetDouble(2);
                dr.Close();

                salar_baza = Convert.ToDouble(textBox5.Text);
                premii_brute = Convert.ToDouble(textBox7.Text);
                spor = Convert.ToDouble(textBox6.Text);
                retineri = Convert.ToDouble(textBox8.Text);
                tot_brut = salar_baza * (1 + spor / 100) + premii_brute;
                CAS = tot_brut * cas;
                CASS = tot_brut * cass;
                brut_imp = tot_brut - CAS - CASS;
                IMP = brut_imp * imp;
                virat_card = tot_brut - IMP - CAS - CASS - retineri;
                p9.Value = tot_brut; 
                p10.Value = brut_imp; 
                p11.Value = virat_card; 
                strSQL = "INSERT INTO ANGAJATI VALUES (:1, :2, :3, :4, :5, :6, :7, :8, :9, :10, :11)";
                command = new OracleCommand(strSQL, conn);
                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                command.Parameters.Add(p3);
                command.Parameters.Add(p4);
                command.Parameters.Add(p5);
                command.Parameters.Add(p6);
                command.Parameters.Add(p7);
                command.Parameters.Add(p8);
                command.Parameters.Add(p9);
                command.Parameters.Add(p10);
                command.Parameters.Add(p11);
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

            //TODO: de facut calculul cu parametrii extrasi din tabela si facut un reloadGrid la final 

            conn.Open();

            double brut_imp = 0;
            double tot_brut = 0;
            double virat_card = 0;
            double salar_baza = 0;
            double premii_brute = 0;
            double retineri = 0;
            double spor = 0;
            double cas = 0;
            double cass = 0;
            double imp = 0;
            double CAS = 0;
            double CASS = 0;
            double IMP = 0;
            OracleCommand command1 = new OracleCommand("SELECT * FROM PROCENTE", conn);
            dr = command1.ExecuteReader();
            dr.Read();
                cas = dr.GetDouble(0);
                cass = dr.GetDouble(1);
                imp = dr.GetDouble(2);  
            dr.Close();

            OracleCommand command2 = new OracleCommand("SELECT * FROM ANGAJATI", conn);          
            dr = command2.ExecuteReader();
            while(dr.Read())
            {
                salar_baza = dr.GetDouble(4);
                premii_brute = dr.GetDouble(6);
                spor = dr.GetDouble(5);
                retineri = dr.GetDouble(7);
                tot_brut = salar_baza * (1 + spor / 100) + premii_brute;
                CAS = tot_brut * cas;
                CASS = tot_brut * cass;
                brut_imp = tot_brut - CAS - CASS;
                IMP = brut_imp * imp;
                virat_card = tot_brut - IMP - CAS - CASS - retineri;            
                
                strSQL = $"UPDATE ANGAJATI SET VIRAT_CARD={virat_card}, TOTAL_BRUT={tot_brut}, BRUT_IMPOZITABIL={brut_imp}  WHERE NUME='{dr.GetString(1)}'";
                OracleCommand command = new OracleCommand(strSQL, conn);
                command.ExecuteNonQuery();
            }
                reloadGrid();
            dr.Close();

            conn.Close();
            label18.Text = "Calcul efectuat";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //TODO: implementeaza cautare si afisare inainte de stergere folosind clauza WHERE
            if (conn.State != ConnectionState.Open)
                conn.Open();

            dataGridView2.ReadOnly = false;
            DialogResult dialog = MessageBox.Show("Doriti stergere?", "Stergere", MessageBoxButtons.YesNo);
            if(dialog == DialogResult.Yes)
            {
                if(ds.Tables["angajati"].Rows.Count>0)
                {
                    int rownum = (dataGridView2.CurrentCell.RowIndex);
                    DataRow row = ds.Tables["angajati"].Rows[rownum];
                    var strSql = $"DELETE FROM ANGAJATI WHERE ID={row.Field<decimal>("id")}";
                    command = new OracleCommand(strSql, conn);
                    command.ExecuteNonQuery();
                    reloadGrid();
                    dataGridView2.ReadOnly = true;

                }
            }
            conn.Close();
            label19.Text = "Angajatul a fost sters cu succes";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }


        private void textBox1_key(object sender, EventArgs e)
        {
            try
            {
                if(conn.State != ConnectionState.Open)
                    conn.Open();

                strSQL = "SELECT * FROM ANGAJATI WHERE NUME LIKE '" + textBox1.Text + "%'";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "angajati");
                dataGridView1.DataSource = ds.Tables["angajati"].DefaultView;
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                strSQL = "SELECT * FROM ANGAJATI WHERE PRENUME LIKE '" + textBox2.Text + "%'";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "angajati");
                dataGridView1.DataSource = ds.Tables["angajati"].DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                strSQL = "SELECT * FROM ANGAJATI WHERE NUME LIKE '" + textBox10.Text + "%'";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "angajati");
                dataGridView2.DataSource = ds.Tables["angajati"].DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                strSQL = "SELECT * FROM ANGAJATI WHERE PRENUME LIKE '" + textBox11.Text + "%'";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "angajati");
                dataGridView2.DataSource = ds.Tables["angajati"].DefaultView;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
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
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                var cell = dataGridView1.CurrentCell;
                    var id = dataGridView1.CurrentRow.Cells[0].Value;
                
                    StringBuilder strSQL = new StringBuilder("UPDATE ANGAJATI SET ", 100);
                    switch (cell.ColumnIndex)
                    {
                    case 0:
                            break;
                        case 1:
                            strSQL.Append($"NUME='{cell.Value}'");
                            break;
                        case 2:
                            strSQL.Append($"PRENUME='{cell.Value}'");
                            break;
                    case 3:
                        strSQL.Append($"FUNCTIE='{cell.Value}'");
                        break;
                    case 4:
                        strSQL.Append($"SALAR_BAZA='{cell.Value}'");
                        break;
                    case 5:
                        strSQL.Append($"SPOR='{cell.Value}'");
                        break;
                    case 6:
                        strSQL.Append($"PREMII_BRUTE='{cell.Value}'");
                        break;
                    case 7:
                        strSQL.Append($"RETINERI='{cell.Value}'");
                        break;
                    default:
                            break;
                    }

                strSQL.Append($" WHERE ID={id}");
                    OracleCommand command = new OracleCommand(strSQL.ToString(), conn);
                    command.ExecuteNonQuery();
                ds.AcceptChanges();
                MessageBox.Show("Actualizat cu succes!");
                    conn.Close();

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
