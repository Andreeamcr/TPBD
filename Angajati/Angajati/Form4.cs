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
    public partial class Form4 : Form
    {
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        String strSQL;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {

                conn = new OracleConnection("USER ID=STUDENT;PASSWORD=student;DATA SOURCE=localhost:1521/XE");
                conn.Open();
                strSQL = "SELECT * FROM ANGAJATI";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "angajati");
                dataGridView1.DataSource = ds.Tables["angajati"].DefaultView;
                conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void textNume_textChanged(object sender, EventArgs e)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                strSQL = "SELECT * FROM ANGAJATI WHERE NUME LIKE '" + textBox1.Text + "%'";
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

        private void textPrenume_textChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int rownum = (dataGridView1.CurrentCell.RowIndex);

                string nrcrt = dataGridView1.Rows[rownum].Cells["NRCRT"].Value.ToString();
                string query = "SELECT * FROM SALARII WHERE nrcrt=" + nrcrt;
                OracleDataAdapter da1 = new OracleDataAdapter(query, conn);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "salarii");

                //DataRow Linie = ds.Tables["salarii"].Rows[rownum];
                CrystalReport1 raport = new CrystalReport1();
                raport.SetDataSource(ds1.Tables["salarii"]);
                crystalReportViewer1.ReportSource = raport;
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new OracleConnection("USER ID=STUDENT;PASSWORD=student;DATA SOURCE=localhost:1521/XE");
                strSQL = "SELECT * FROM ANGAJATI";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "angajati");
                dataGridView1.DataSource = ds.Tables["angajati"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            CrystalReport2 raport = new CrystalReport2();
            raport.SetDataSource(ds.Tables["angajati"]);
            crystalReportViewer1.ReportSource = raport;
        }
    }
    }
    

