﻿using System;
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
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        public DataProcessing dp;
        public Form1()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;

            
           
            
            dp = new DataProcessing();
            dp.LoadData();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

          
            this.BackColor = Color.Black;
            graph_table.Size = new Size(this.Size.Width, graph_table.Size.Height);
            graph_table.Location = new Point(0, graph_table.Location.Y);


            LabelList.Location = new Point((int)(this.Width * 0.005), LabelList.Location.Y);
            LabelList.Size = new Size((int)(this.Width * 0.99), LabelList.Height );
            
            foreach (string s in dp.label)
                LabelList.Items.Add(s);



            LabelView.Location = new Point((int)(this.Width * 0.005), LabelView.Location.Y);
            LabelView.Size = new Size((int)(this.Width * 0.99), LabelView.Height);
                     
            foreach (string s in dp.label)
                LabelView.Items.Add(new ListViewItem(s));

            LabelList.Visible = false;
            //LabelView.Visible = false;
            //HorizontalListbox();
        }

      

        public void HorizontalListbox()
        {

            LabelView.View = View.Tile;
            LabelView.Alignment = ListViewAlignment.Left;
            LabelView.OwnerDraw = true;
            LabelView.DrawItem += listView1_DrawItem;
            LabelView.Location = new Point((int)(this.Width * 0.025), LabelView.Location.Y);
            LabelView.Size=new Size((int)(this.Width*0.95), LabelView.Height*2);
            LabelView.TileSize = new Size(48,
            LabelView.ClientSize.Height - (SystemInformation.HorizontalScrollBarHeight + 4));
            foreach (string s in dp.label)
            {
                string tmp = "";
                foreach (char c in s)
                    tmp += (c.ToString() + '\n');
                LabelView.Items.Add(new ListViewItem(tmp));
            }
        }

        public void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Color textColor = Color.Black;
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                textColor = SystemColors.HighlightText;
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            e.Graphics.DrawRectangle(Pens.DarkGray, e.Bounds);

            TextRenderer.DrawText(e.Graphics, e.Item.Text, LabelView.Font, e.Bounds,
                                  textColor, Color.Empty,
                                  TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void LabelView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            graph_table.Reset();


            List<int> select_index = new List<int>();
            //foreach (int i in LabelList.SelectedIndices)
            //select_index.Add(i);

            foreach (int i in LabelView.CheckedIndices)
                select_index.Add(i);

            if(select_index.Count<2)
            {
                MessageBox.Show("Please pick more than 2 category!");
                return;
            }

            if (preTime.Value > curTime.Value)
            {
                DateTime tmpdatetime = preTime.Value;
                preTime.Value = curTime.Value;
                curTime.Value = tmpdatetime;
            }
           

            List<string> label_name = new List<string>();

            foreach (int index in select_index)
                label_name.Add(dp.label[index]);

            List<Data> tmpData = new List<Data>();


            Console.WriteLine(preTime.Value.Year + "/" + preTime.Value.Month + "/" + preTime.Value.Day);
            Console.WriteLine(curTime.Value.Year + "/" + curTime.Value.Month + "/" + curTime.Value.Day);

          
            tmpData = dp.SearchData(preTime.Value.Year + "/" + preTime.Value.Month + "/" + preTime.Value.Day
                                    , curTime.Value.Year + "/" + curTime.Value.Month + "/" + curTime.Value.Day);

            if (tmpData.Count <1)
            {
                MessageBox.Show("There is no data in the range!");
                return;
            }

            //tmpData = dp.SearchData("2007/1/2", "2007/1/10");
            dp.CalculateLabelRange(tmpData);
          
            //dp.CalculateLabelRange();
            //dp.TestFunc();

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
            graph_table.isExist = true;
            graph_table.drawGraph();
        }

        private void curTime_DragOver(object sender, DragEventArgs e)
        {

        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
         
            //graph_table.SuspendLayout();
        }

        private void Form1_DragLeave(object sender, EventArgs e)
        {
            //graph_table.ResumeLayout();
        }

        private void graph_table_Load(object sender, EventArgs e)
        {

        }

    }
}
