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
    public partial class Form5 : Form
    {
        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        String strSQL;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
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
            CrystalReport1 raport = new CrystalReport1();
            raport.SetDataSource(ds.Tables["angajati"]);
            crystalReportViewer1.ReportSource = raport;
        }
    }
}
