using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Angajati
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string parola;
            parola = textBox1.Text;
            
            if (parola == "student")
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.ShowDialog();
                this.Close();


            }
            else MessageBox.Show("Parola incorecta");
        }
    }
}
