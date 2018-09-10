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
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        public DataProcessing dp;
        public int modeValue = 0;
        public int Max_Mode = 3;
        public List<string> mode_name;
        public string firstLabel = "日期";
        public List<List<Control>> Panel_Layer; 
        

        public Form1()
        {
            InitializeComponent();
            TestFunc();
            Panel_Layer=new List<List<Control>>();

            dp = new DataProcessing();
            
            //Load by python script
            /*
            ExecutePython e = new ExecutePython();
            e.Ececute();
            e.TestFunc();
            dp.SetByPreprocessData(e.label, e.date, e.data);
            */

            //Load by C# function
            dp.LoadData();

            mode_name = new List<string>();
            mode_name.Add("Mode : Gradient by two colors.");
            mode_name.Add("Mode : Gradient by alpha.");
            mode_name.Add("Mode : Gradient by multiple colors.");
        }

        public void TestFunc()
        {
            List<float> x = new List<float>();
            List<float> y = new List<float>();
            x.Add(43);
            x.Add(21);
            x.Add(25);
            x.Add(42);
            x.Add(57);
            x.Add(59);
           

            y.Add(99);
            y.Add(65);
            y.Add(79);
            y.Add(75);
            y.Add(87);
            y.Add(81);

            CorrelationCoefficient cc = new CorrelationCoefficient(x, y);

            Console.WriteLine(cc.CalculateCC().ToString());
        }



        //Redraw the control components to the correspond size
        private void Form1_Load(object sender, EventArgs e)
        {

            initFirstLayer();
            initSecondLayer();
            ShowLayer(0);

           

        }
        

        public void initFirstLayer()
        {
            //this.BackColor = Color.Black;
            graph_table.Size = new Size(this.Size.Width, (int)(this.Size.Height * 0.48));
            graph_table.Location = new Point(0, (int)(this.Height * 0.09));


            /*
            LabelList.Location = new Point((int)(this.Width * 0.005), LabelList.Location.Y);
            LabelList.Size = new Size((int)(this.Width * 0.99), LabelList.Height );
            
            foreach (string s in dp.label)
                LabelList.Items.Add(s);
            */


            LabelView.Location = new Point((int)(this.Width * 0.005), (int)(this.Height * 0.6));
            LabelView.Size = new Size((int)(this.Width * 0.99), (int)(this.Height * 0.3));

            LabelView.Items.Add(new ListViewItem(firstLabel));
            foreach (string s in dp.label)
                LabelView.Items.Add(new ListViewItem(s));




            LabelList.Visible = false;

            graph_table.Init();

            preTime.Location = new Point(preTime.Location.X, this.Height - preTime.Height * 2);
            curTime.Location = new Point(curTime.Location.X, this.Height - curTime.Height * 2);
            updateBtn.Location = new Point(this.Width - (int)(updateBtn.Width * 1.5), (int)(this.Height * 0.94));

            alphaBar.Location = new Point((int)(this.Width - alphaBar.Width * 1.1), (int)(this.Height * 0.05));
            alphaBar.Value = 100;

            mode.Location = new Point((int)(this.Width - alphaBar.Width * 1.1 - mode.Width * 1.3), (int)(this.Height * 0.045));


            List<Control> lc = new List<Control>();
            lc.Add(graph_table);
            lc.Add(preTime);
            lc.Add(curTime);
            lc.Add(updateBtn);
            lc.Add(alphaBar);
            lc.Add(mode);
            lc.Add(LabelView);

            Panel_Layer.Add(lc);
        }
        

        public void initSecondLayer()
        {

            int marg;

            scatterPlot.Size = new Size((int)(this.Width * 0.7), (int)(this.Height * 0.8));
            scatterPlot.Location = new Point(0, (int)(this.Height * 0.1));

            LabelView2.Size = new Size((int)(this.Width * 0.3), (int)(this.Height * 0.8));
            LabelView2.Location = new Point((int)(this.Width * 0.7), (int)(this.Height * 0.1));
            updateScatterPlot.Location = new Point(this.Width - (int)(updateBtn.Width * 1.5), (int)(this.Height * 0.94));

            foreach (string s in dp.label)
                LabelView2.Items.Add(new ListViewItem(s));



            List<Control> lc = new List<Control>();
            lc.Add(scatterPlot);
            lc.Add(LabelView2);
            lc.Add(updateScatterPlot);

            Panel_Layer.Add(lc);
            
        }

        public void ShowLayer(int index)
        {
            for(int i=0;i<Panel_Layer.Count;i++)
            {
                foreach(Control c in Panel_Layer[i])
                {
                    if (i == index)
                        c.Visible = true;
                    else
                        c.Visible = false;
                }
            }
        }


        /*
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
        */
        private void LabelView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //Handle the update button
        //Update the data that satisfy the condition 
        private void metroButton1_Click(object sender, EventArgs e)
        {
            graph_table.Reset();


            List<int> select_index = new List<int>();


            bool useDateInfo=false;
            foreach (int i in LabelView.CheckedIndices)
            {
                if (i != 0)
                    select_index.Add(i-1);
                else
                    useDateInfo = true;
            }

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

            /*
            Console.WriteLine(preTime.Value.Year + "/" + preTime.Value.Month + "/" + preTime.Value.Day);
            Console.WriteLine(curTime.Value.Year + "/" + curTime.Value.Month + "/" + curTime.Value.Day);
            */
          
            tmpData = dp.SearchData(preTime.Value.Year + "/" + preTime.Value.Month + "/" + preTime.Value.Day
                                    , curTime.Value.Year + "/" + curTime.Value.Month + "/" + curTime.Value.Day);

            if (tmpData.Count <1)
            {
                MessageBox.Show("There is no data in the range!");
                return;
            }

            dataNumLabel.Text = "Number of Data : " + tmpData.Count;

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



            //insert date 
            int add = 0;
            if (useDateInfo)
            {
                for (int i = 0; i < tmpdata.Count; i++)
                    tmpdata[i].Insert(0, i + 1);
                label_axis.Insert(0, dp.GetDateLabel(tmpdata.Count));
                label_name.Insert(0, firstLabel);
                add = 1;
                graph_table.SetDateInfo(tmpData[tmpData.Count-1].time, tmpData[0].time, true);
            }else
            {
                graph_table.SetDateInfo("", "", false);
            }
            graph_table.InsertData(select_index.Count+add, tmpData.Count, label_axis, tmpdata, label_name);
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

        private void LabelList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void alphaBar_Scroll(object sender, ScrollEventArgs e)
        {
            graph_table.AlphaChange((int)(alphaBar.Value * 2.55));
        }

        private void mode_Click(object sender, EventArgs e)
        {
            modeValue++;
            int m = modeValue  % Max_Mode;

            
            mode.Text = mode_name[m];
            graph_table.SetMode(m);
            

        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            scatterPlot.Rest();
            scatterPlot.isExist = true;
            List<int> select_index = new List<int>();
            foreach (int i in LabelView2.CheckedIndices)
                select_index.Add(i);
            
            if(select_index.Count>7||select_index.Count<2)
            {
                MessageBox.Show("Numbers of category must be between 2 to 7");
                return;
            }


            List<string> label_name = new List<string>();
            List<List<float>> data=new List<List<float>>();
            foreach (int i in select_index)
            {
                label_name.Add(dp.label[i]);
                data.Add(dp.ReturnRowData(i));

            }

            scatterPlot.Init(select_index.Count);
            scatterPlot.SetLabelData(data, label_name);

            
            scatterPlot.NotifyRedraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    RightClickMenu.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                }
                break;
            }
        }

        private void graphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Graph";
            this.Invalidate();
            ShowLayer(0);
        }

        private void correlationCoefficientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "Correlation Coefficient";
            this.Invalidate();
            ShowLayer(1);
        }

       

    }
}
