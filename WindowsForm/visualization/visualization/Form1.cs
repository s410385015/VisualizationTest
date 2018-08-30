using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualization
{
    public partial class Form1 : Form
    {

        public DataProcessing dp;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dp = new DataProcessing();
            dp.LoadData();

            foreach (string s in dp.label)
                LabelList.Items.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graph_table.TestFunc();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            graph_table.TestFunc2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
