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
        public List<CCGroup> ccLabel;
        public List<string> ccLabel_name;
        public bool first_layer_LabelView_mode;
        public bool useDateInfo;
        public bool isHor;
        public Form1()
        {
            InitializeComponent();
            
                
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

            ccLabel = new List<CCGroup>();
            ccLabel_name = new List<string>();
            LoadCC();
            isHor = true;
           
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
            Loading.Size = new Size(this.Width, this.Height);
            Loading.Location = new Point(0,2);
            initFirstLayer();
            initSecondLayer();
            ShowLayer(0);
            Loading.BringToFront();
            //LoadCCLabel();
           
           

        }
        

        public void initFirstLayer()
        {
            //this.BackColor = Color.Black;
            graph_table.Size = new Size(this.Size.Width, (int)(this.Size.Height * 0.48));
            graph_table.Location = new Point(0, (int)(this.Height * 0.09));
            graph_table.rce = graph_table_MouseDown; 

            /*
            LabelList.Location = new Point((int)(this.Width * 0.005), LabelList.Location.Y);
            LabelList.Size = new Size((int)(this.Width * 0.99), LabelList.Height );
            
            foreach (string s in dp.label)
                LabelList.Items.Add(s);
            */

            LabelView.BringToFront();
           
            LabelViewCC.Location = new Point((int)(this.Width * 0.005), (int)(this.Height * 0.6));
            LabelViewCC.Size = new Size((int)(this.Width * 0.99), (int)(this.Height * 0.3));
            
            LabelViewCC.Visible = false;



            
          
            


            LabelView.Location = new Point((int)(this.Width * 0.005), (int)(this.Height * 0.6));
            LabelView.Size = new Size((int)(this.Width * 0.99), (int)(this.Height * 0.3));

            LabelView.Items.Add(new ListViewItem(firstLabel));
            foreach (string s in dp.label)
                LabelView.Items.Add(new ListViewItem(s));

           

            
          
            
           
            first_layer_LabelView_mode = true;


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
            lc.Add(dataNumLabel);
            lc.Add(LabelViewCC);
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
            lc.Add(alphaBar);
            Panel_Layer.Add(lc);
            
        }

        public void ShowLayer(int index)
        {
            for(int i=0;i<Panel_Layer.Count;i++)
            {
                foreach(Control c in Panel_Layer[i])
                {
                   
                        c.Visible = false;
                }
            }

            foreach (Control c in Panel_Layer[index])
            {
                 c.Visible = true;
            }
        }


       
        private void LabelView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //Handle the update button
        //Update the data that satisfy the condition 
        private void metroButton1_Click(object sender, EventArgs e)
        {
            graph_table.Reset();


            List<int> select_index = new List<int>();

            
            useDateInfo=false;
            if (first_layer_LabelView_mode)
            {
                foreach (int i in LabelView.CheckedIndices)
                {
                    if (i != 0)
                        select_index.Add(i - 1);
                    else
                        useDateInfo = true;
                }
            }else
            {
                select_index=SelectFromCCLabel();
            }

            if(select_index.Count<2)
            {
                if (select_index.Count == 0 || useDateInfo == false)
                {
                    MessageBox.Show("Please pick more than 2 category!");
                    return;
                }
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
            scatterPlot.AlphaChange((int)(alphaBar.Value * 2.55));
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


       
        public void LoadCC()
        {
            for (int i = 0; i < dp.labelNum; i++)
            {
                for (int j = i + 1; j < dp.labelNum; j++)
                {
                    if (j != i)
                    {
                        CorrelationCoefficient cc = new CorrelationCoefficient(dp.ReturnRowData(i), dp.ReturnRowData(j));
                        CCGroup ccg = new CCGroup(i, j, cc.CalculateCC(),cc.CalculateSimilarity());
                        
                        //if the match is >0.7, means it is over match, ingore this two labels.
                        if(ccg.match<=0.7)
                            ccLabel.Add(ccg);
                    }
                }
            }


            //sort cc array

            for(int i=0;i<ccLabel.Count;i++)
            {
                for(int j=i+1;j<ccLabel.Count;j++)
                {
                    if(Math.Abs(ccLabel[j].value)>Math.Abs(ccLabel[i].value))
                    {
                        CCGroup tmp = ccLabel[j];
                        ccLabel[j] = ccLabel[i];
                        ccLabel[i] = tmp;
                    }
                }
            }

            for (int i = 0; i < ccLabel.Count; i++)
            {

                //string s = dp.label[ccLabel[i].indexA] + "+" + dp.label[ccLabel[i].indexB] + "(" + ccLabel[i].value.ToString("0.00") + "-"+ccLabel[i].match.ToString("0.00")+")";
                string s = dp.label[ccLabel[i].indexA] + "+" + dp.label[ccLabel[i].indexB] + "(" + ccLabel[i].value.ToString("0.00") + ")";
                ccLabel_name.Add(s);
            }
        }

        private  void LoadCCLabel()
        {
            LabelViewCC.Invoke(new MethodInvoker(delegate() { LabelViewCC.Items.Add(new ListViewItem("日期")); }));
            //List<string> tmp = new List<string>();    
            for (int i = 0; i < ccLabel.Count; i++)
            {
                   
                //tmp.Add(s);
                LabelViewCC.Invoke(new MethodInvoker(delegate() { LabelViewCC.Items.Add(new ListViewItem(ccLabel_name[i])); }));
            }


            //LabelViewCC.Invoke(new MethodInvoker(delegate() { LabelViewCC.Items.Add(new ListViewItem(tmp.ToArray())); }));
            Loading.Invoke(new MethodInvoker(delegate() { Loading.Visible = false; }));
            
        }

        public void TestFuncLoad()
        {


            //LabelViewCC.BeginUpdate();
            LabelViewCC.Items.Add("日期");
            foreach (string s in ccLabel_name)
                LabelViewCC.Items.Add(s);
            //LabelViewCC.EndUpdate();


  
            Loading.Visible = false; 
        
        }
        /*
        private void TestFuncLoad()
        {
            this.Invoke(new MethodInvoker(delegate() {

                
                
                LabelViewCC.BeginUpdate();
                LabelViewCC.Items.Add("日期");
                foreach (string s in ccLabel_name)
                    LabelViewCC.Items.Add(s);
                LabelViewCC.EndUpdate();

               
                //LabelViewCC.Invoke(new MethodInvoker(delegate() { LabelViewCC.Items.Add(new ListViewItem(tmp.ToArray())); }));
                Loading.Visible = false; 
            }));
        }
        */

        private void LabelViewMode_Opening(object sender, CancelEventArgs e)
        {

        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LabelView.Visible = true;
            LabelViewCC.Visible = false;
            first_layer_LabelView_mode = true;
        }

        private void sortByCCToolStripMenuItem_Click(object sender, EventArgs e)
        {


            LabelView.Visible = false;
            LabelViewCC.Visible = true;
            first_layer_LabelView_mode = false;
        }

        private void LabelView_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        
                        LabelViewMode.Show(LabelView, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void LabelViewCC_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {

                        LabelViewMode.Show(LabelView, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
            var thread = new Thread(LoadCCLabel);
            //var thread = new Thread(TestFuncLoad);
           
            thread.Start();
            //Loading.Visible = false;
            //Action action = TestFuncLoad;
            //action.BeginInvoke(ar => action.EndInvoke(ar), null);
        }

        public List<int> SelectFromCCLabel()
        {
            List<int> selected = new List<int>();

            foreach (int i in LabelViewCC.CheckedIndices)
            {
                if (i == 0)
                {
                   
                    useDateInfo = true;
                }
                else
                {
                    if (!selected.Contains(ccLabel[i - 1].indexA))
                        selected.Add(ccLabel[i - 1].indexA);
                    if (!selected.Contains(ccLabel[i - 1].indexB))
                        selected.Add(ccLabel[i - 1].indexB);
                }
            }
            return selected;
        }

        private void Loading_Click(object sender, EventArgs e)
        {
            Console.WriteLine("??");
        }
        
        public void graph_table_SetScreen(float f)
        {
            graph_table.Size = new Size(this.Size.Width, (int)(this.Size.Height * f));
        }

        private void graph_table_MouseDown(object sender, MouseEventArgs e)
        {
           
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                       
                        graph_table_mode.Show(graph_table, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void fullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph_table_SetScreen(0.9f);
            graph_table.BringToFront();
            graph_table.ReInit();
            graph_table.NotifyRedraw();
        }

        private void halfToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            graph_table_changeDir(isHor);
        }


        private void horizToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph_table_changeDir(true);
        
        }

        private void useRayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            graph_table.useRay = !graph_table.useRay;
            if (graph_table.useRay)
            {
                useRayToolStripMenuItem.Text = "Cancel ray";

            }
            else
            {
                useRayToolStripMenuItem.Text = "Use ray";
                graph_table.NotifyRedraw();
            }
        }


 
        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graph_table_changeDir(false);
        }

        private void directionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        public void graph_table_changeDir(bool flag)
        {
            if(flag)
            {
                graph_table.Size = new Size(this.Size.Width, (int)(this.Size.Height * 0.48));
                graph_table.Location = new Point(0, (int)(this.Height * 0.09));
                LabelView.BringToFront();
                LabelViewCC.Location = new Point((int)(this.Width * 0.005), (int)(this.Height * 0.6));
                LabelViewCC.Size = new Size((int)(this.Width * 0.99), (int)(this.Height * 0.3));

                LabelView.Location = new Point((int)(this.Width * 0.005), (int)(this.Height * 0.6));
                LabelView.Size = new Size((int)(this.Width * 0.99), (int)(this.Height * 0.3));


                graph_table.SetDirection(true);
                isHor = true;
            }
            else
            {
                graph_table.Size = new Size((int)(this.Width * 0.7), (int)(this.Height * 0.8));
                graph_table.Location = new Point(0, (int)(this.Height * 0.09));
                LabelView.BringToFront();

                LabelViewCC.Size = new Size((int)(this.Width * 0.3), (int)(this.Height * 0.8));
                LabelViewCC.Location = new Point((int)(this.Width * 0.7), (int)(this.Height * 0.09));
                LabelView.Size = new Size((int)(this.Width * 0.3), (int)(this.Height * 0.8));
                LabelView.Location = new Point((int)(this.Width * 0.7), (int)(this.Height * 0.09));



                graph_table.SetDirection(false);
                isHor = false;
            }
        }
    }
}
