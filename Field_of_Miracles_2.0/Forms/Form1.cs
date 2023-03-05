using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Field_of_Miracles_2._0
{
    public partial class Form1 : Form
    {
        Thread th;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(open);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void open(object obg)
        {
            Application.Run(new Form2());
        }

        private void главноеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void оАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void оИгреToolStripMenuItem_Click(object sender, EventArgs e)
        {
            th = new Thread(open_1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void open_1(object obg)
        {
            Application.Run(new Form3());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void оНасToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
