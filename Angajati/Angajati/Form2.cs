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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
        }

        OracleConnection conn;
        OracleDataAdapter da;
        DataSet ds;
        string strSQL;
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                
                conn = new OracleConnection("USER ID=STUDENT;PASSWORD=student;DATA SOURCE=localhost:1521/XE");
                conn.Open();
                strSQL = "SELECT * FROM Procente";
                da = new OracleDataAdapter(strSQL, conn);
                ds = new DataSet();
                da.Fill(ds, "procente");
                dataGridView1.DataSource = ds.Tables["procente"].DefaultView;
                conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO: implement only float inputs
            try
            {
                ds.RejectChanges();
                dataGridView1.ReadOnly = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                var cell = dataGridView1.CurrentCell;
                StringBuilder strSQL = new StringBuilder("UPDATE PROCENTE SET ", 100);
                switch (cell.ColumnIndex)
                {
                    case 0:
                        strSQL.Append($"CAS={cell.Value}");
                        break;
                    case 1:
                        strSQL.Append($"CASS={cell.Value}");
                        break;
                    case 2:
                        strSQL.Append($"IMPOZIT={cell.Value}");
                        break;
                    default:
                        break;
                }
                OracleCommand command = new OracleCommand(strSQL.ToString(), conn);
                command.ExecuteNonQuery();

                MessageBox.Show("Actualizat cu succes!"); 
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
