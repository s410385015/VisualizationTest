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

        private void Update_Click(object sender, EventArgs e)
        {
            graph_table.Reset();


            List<int> select_index=new List<int>();
            foreach (int i in LabelList.SelectedIndices)
                select_index.Add(i);


            List<string> label_name = new List<string>();

            foreach (int index in select_index)
                label_name.Add(dp.label[index]);

            List<Data> tmpData = new List<Data>();
            tmpData = dp.SearchData("2007/1/2", "2007/1/10");
            dp.CalculateLabelRange(tmpData);
            //dp.CalculateLabelRange();
            dp.TestFunc();

            List<List<float>> tmpdata = new List<List<float>>();

            for (int i = 0; i < tmpData.Count; i++)
            {
                List<float> tmp = new List<float>();
                foreach (int index in select_index)
                    tmp.Add(tmpData[i].data[index]);
                tmpdata.Add(tmp);
            }



            List<List<float>> label_axis = new List<List<float>>();

            foreach (int index in select_index)
                label_axis.Add(dp.labelRange[index].value);

            graph_table.InsertData(select_index.Count, tmpData.Count, label_axis, tmpdata, label_name);

            graph_table.Update();
        }
    }
}
