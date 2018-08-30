using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualization
{
    


    public partial class Graph : UserControl
    {

        public int w;
        public int h;
        


        public int margin_top;
        public int margin_bottom;
        public int margin_left;
        public int margin_right;

        int startX, startY;
        public Color axis_color;
        public float axis_size;

        //axis length between each others
        public int axis_length;
        public int axis_height;


        


        public int label_axis_num;
        public int label_axis_size;
        public int label_axis_width;
        public Font label_axis_font;
        public SolidBrush label_axis_brush;
        public StringFormat label_axis_format;
        public int label_axis_sizeOflabel;
        public int label_axis_heightOflabel;
        public int label_axis_widthOflabel;


        public int class_num;
        public int label_size;
        public int label_height;
        public int label_width;
        public Font label_font;
        public Color label_color_org;
        public Color label_color_select;
        public List<string> label_name;
        
        
        public int tmp_select;
        public List<DataLabel> data;

        public Graphics g;

        public Color data_old;
        public Color data_new;
        public int data_line_size;
        public int data_num;


        public Graph()
        {
            InitializeComponent();
            //Init();
           // GraphInit();
           
        }

        

        public void Init()
        {
            tmp_select = -1;

            w = this.Size.Width;
            h = this.Size.Height;

            margin_top = 20;
            margin_bottom = 10;
            margin_left = 50;
            margin_right = 50;

            axis_size = 3;
            axis_color = Color.Black;
            
            
          

            label_axis_size = 3;
            label_axis_width = 5;
            label_axis_num = 10;
            label_axis_sizeOflabel = 8;
            label_axis_heightOflabel = 15;
            label_axis_widthOflabel = 40;
            label_axis_font = new Font("Arial", label_axis_sizeOflabel);
            label_axis_brush = new SolidBrush(Color.Black);
            label_axis_format = new StringFormat();
            label_axis_format.Alignment = StringAlignment.Far;

           
            
            
            class_num = 3;
            label_size = 5;
            label_height = 20;
            label_width = 30;
            label_font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            data = new List<DataLabel>();
            label_name = new List<string>();

            label_color_org = Color.Black;
            label_color_select = Color.Red;

            data_new = Color.FromArgb(255, 0, 0);
            data_old = Color.FromArgb(0, 0, 255);
            data_line_size = 1;
            data_num = 7;

            axis_length = (w - margin_left - margin_right) / (class_num - 1);
            axis_height = this.Size.Height - margin_top - margin_bottom - label_height;
            
        }


        //lb label axis data
        //d  data
        //ln label name
        public void InsertData(int cn, int dn, List<List<float>> lb, List<List<float>> d,List<string> ln)
        {
            class_num = cn;
            data_num = dn;

            axis_length = (w - margin_left - margin_right) / (class_num - 1);


          
            label_name = new List<string>();
            data = new List<DataLabel>();


            label_name = ln;

            for(int i=0;i<cn;i++)
            {
                DataLabel datatmp = new DataLabel();
                for (int j = 0; j < lb[i].Count; j++)
                    datatmp.data_label.Add(lb[i][j]);

                for (int j = 0; j < dn; j++)
                    datatmp._data.Add(d[j][i]);

                data.Add(datatmp);
            }

         
        }

        public void Update()
        {
            g.Clear(this.BackColor);
            GraphInit();
            DrawAxis();
            for (int i = 0; i < class_num - 1; i++)
                DrawDataLine(i);
                
        }
        
        public void Reset()
        {
            foreach (DataLabel dl in data)
                this.Controls.Remove(dl.label);
            data.Clear();
            label_name.Clear();
        }

        public void GenerateFakeData()
        {
           
            DataLabel data1 = new DataLabel();
            DataLabel data2 = new DataLabel();
            DataLabel data3 = new DataLabel();
            
            for (int i = 0; i < 10; i++)
                data1.data_label.Add(0.1f*i);

            for (int i = 0; i < 3; i++)
                data2.data_label.Add(1f * i);

            for (int i = 0; i < 5; i++)
                data3.data_label.Add(0.5f * i);



            data1._data.Add(0.05f);
            data1._data.Add(0.2f);
            data1._data.Add(0.3f);
            data1._data.Add(0.4f);
            data1._data.Add(0.56f);
            data1._data.Add(0.73f);
            data1._data.Add(0.82f);


            data2._data.Add(0.2f);
            data2._data.Add(0.5f);
            data2._data.Add(0.73f);
            data2._data.Add(0.99f);
            data2._data.Add(1.15f);
            data2._data.Add(1.22f);
            data2._data.Add(1.58f);

            data3._data.Add(0.3f);
            data3._data.Add(0.43f);
            data3._data.Add(0.5f);
            data3._data.Add(0.73f);
            data3._data.Add(0.99f);
            data3._data.Add(1.23f);
            data3._data.Add(1.58f);


            data.Add(data1);
            data.Add(data2);
            data.Add(data3);



            

           
        }

        public void GraphInit()
        {
            g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.DrawRectangle(new Pen(axis_color, axis_size), new Rectangle(50, 50, 100, 100));
        }

        public void DrawAxis()
        {

            startX = margin_left;
            startY = margin_top;

            Pen pen = new Pen(axis_color, axis_size);
            for(int i=0;i<class_num;i++)
            {
                
                Point p1=new Point(startX+i*axis_length,startY);
                Point p2=new Point(startX+i*axis_length,startY+axis_height);

                //axis label length between each others
                int axis_label_length = axis_height / (data[i].data_label.Count -1);





                for (int j = 0; j < data[i].data_label.Count; j++)
                {

                    Point p3 = new Point(p1.X - label_axis_width, p2.Y - j * axis_label_length);
                    Point p4 = new Point(p1.X, p2.Y - j * axis_label_length);
                    g.DrawLine(pen, p3, p4);

                    RectangleF drawRect = new RectangleF(p1.X - label_axis_widthOflabel - label_axis_width, p2.Y - j * axis_label_length - label_axis_sizeOflabel , label_axis_widthOflabel, label_axis_heightOflabel);

                    g.DrawString(data[i].data_label[j].ToString(), label_axis_font, label_axis_brush, drawRect, label_axis_format);
                }

                GenerateLabel(new Point(p1.X-label_width, p2.Y +label_height/2), i);
                g.DrawLine(pen, p1, p2);
            }


        }

        private void Graph_Load(object sender, EventArgs e)
        {
            
            Init();
            GenerateFakeData();
            GraphInit();
         
 
           
        }

        private void Graph_MouseClick(object sender, MouseEventArgs e)
        {
           
            //g.DrawLine(new Pen(Color.Black,20),new Point(0,0), new Point(100,100));
        }

        private void Graph_Paint(object sender, PaintEventArgs e)
        {
           // DrawAxis();
            //for (int i = 0; i < class_num-1; i++)
                //DrawDataLine(i);
        }

        public void GenerateLabel(Point p,int index)
        {

            data[index].label = new Label();
            data[index].label.Font = label_font;
            data[index].label.Location = p;
            if (label_name.Count > 0)
                data[index].label.Text = label_name[index];
            else
                data[index].label.Text = "Label:" + index.ToString();
           
            data[index].label.Name = index.ToString();
            data[index].label.Click += new EventHandler(label_Click);

            this.Controls.Add(data[index].label);
        }



        public void label_Click(object sender, EventArgs e)
        {

                Label l = sender as Label;
                MouseEventArgs me=(MouseEventArgs) e;
                int i = 0;

                for (int j = 0; j < data.Count; j++)
                    if (data[j].isObj(l))  
                        i = j;
                Console.WriteLine("pre select " + tmp_select);
              
                
   
                if (me.Button == MouseButtons.Left)
                {
                    if (l.ForeColor == label_color_select)
                    {
                        
                        data[i].Reverse();
                        DestoryAxis(i);
                        DestroyLineData(i);
                        DestroyLineData(i - 1);
                        
                        RedrawAxis(i);
                        RedrawAxis(i+1);
                        DrawDataLine(i);
                        DrawDataLine(i-1);

                        l.ForeColor = label_color_org;
                        tmp_select =-1;
                    }
                    else
                    {


                        if (tmp_select == -1)
                        {
                            l.ForeColor = label_color_select;
                            tmp_select = i;
                        }
                        else
                        {


                            DestoryAxis(i);
                            DestoryAxis(tmp_select);
                            DestroyLineData(i);
                            DestroyLineData(i-1);
                            DestroyLineData(tmp_select);
                            DestroyLineData(tmp_select-1);

                            SwapData(i, tmp_select);


                            RedrawAxis(i);
                            RedrawAxis(i+1);
                            RedrawAxis(tmp_select);
                            RedrawAxis(tmp_select+1);
                            DrawDataLine(i);
                            DrawDataLine(i-1);
                            DrawDataLine(tmp_select);
                            DrawDataLine(tmp_select - 1);

                            DisSelectLabel(i, tmp_select);
                            


                            tmp_select = -1;
                        }

                    }
                    
                }
                else if (me.Button == MouseButtons.Right)
                    l.ForeColor = label_color_org;

                
        }

        public void DestoryAxis(int index)
        {
            startX = margin_left + index * axis_length;
            startY = margin_top;
            Brush brush = new SolidBrush(this.BackColor);

            //Console.WriteLine((startX - label_axis_widthOflabel * 2) + " " + (startY - 1) + " " + (label_axis_widthOflabel * 4) + " " + (startY + axis_height + 1));
            g.FillRectangle(brush, startX - label_axis_widthOflabel * 2, startY - label_axis_heightOflabel, label_axis_widthOflabel * 4, startY + axis_height + 1);
            //g.DrawRectangle(pen, startX - label_axis_widthOflabel * 2, startY - 1, label_axis_widthOflabel * 4, startY + axis_height + 1);
          
        }

        public void DisSelectLabel(int indexA,int indexB)
        {
      
            data[indexA].label.ForeColor = label_color_org;
            data[indexB].label.ForeColor = label_color_org;
           
            
        }

        


        public void RedrawAxis(int index)
        {

            if (index < class_num && index >= 0)
            {
                //true : bottom to top
                //false: up to bottom
                bool flag = true;

                if (data[index].data_label.Count > 1)
                    flag = (data[index].data_label[1] > data[index].data_label[0]) ? (true) : (false);


                startX = margin_left;
                startY = margin_top;

                Pen pen = new Pen(axis_color, axis_size);

                Point p1 = new Point(startX + index * axis_length, startY);
                Point p2 = new Point(startX + index * axis_length, startY + axis_height);

                //axis label length between each others
                int axis_label_length = axis_height / (data[index].data_label.Count - 1);




                if (flag)
                {
                    for (int j = 0; j < data[index].data_label.Count; j++)
                    {

                        Point p3 = new Point(p1.X - label_axis_width, p2.Y - j * axis_label_length);
                        Point p4 = new Point(p1.X, p2.Y - j * axis_label_length);
                        g.DrawLine(pen, p3, p4);

                        RectangleF drawRect = new RectangleF(p1.X - label_axis_widthOflabel - label_axis_width, p2.Y - j * axis_label_length - label_axis_sizeOflabel, label_axis_widthOflabel, label_axis_heightOflabel);

                        g.DrawString(data[index].data_label[j].ToString(), label_axis_font, label_axis_brush, drawRect, label_axis_format);
                    }
                }
                else
                {
                    for (int j = 0; j < data[index].data_label.Count; j++)
                    {

                        Point p3 = new Point(p1.X - label_axis_width, p1.Y + j * axis_label_length);
                        Point p4 = new Point(p1.X, p1.Y + j * axis_label_length);
                        g.DrawLine(pen, p3, p4);

                        RectangleF drawRect = new RectangleF(p1.X - label_axis_widthOflabel - label_axis_width, p1.Y + j * axis_label_length - label_axis_sizeOflabel, label_axis_widthOflabel, label_axis_heightOflabel);

                        g.DrawString(data[index].data_label[data[index].data_label.Count - j - 1].ToString(), label_axis_font, label_axis_brush, drawRect, label_axis_format);
                    }
                }

                //GenerateLabel(new Point(p1.X - label_width, p2.Y + label_height / 2), index);
                g.DrawLine(pen, p1, p2);
            }
        }




        public void DrawDataLine(int index)
        {

            if (index + 1< class_num && index>=0)
            {
                startX = margin_left;
                startY = margin_top;


                //true : bottom to top
                //false: up to bottom
                bool flag_left = true;
                bool flag_right = true;

                if (data[index].data_label.Count > 1)
                    flag_left = (data[index].data_label[1] > data[index].data_label[0]) ? (true) : (false);

                if (data[index + 1].data_label.Count > 1)
                    flag_right = (data[index + 1].data_label[1] > data[index + 1].data_label[0]) ? (true) : (false);



                int left_x = startX + index * axis_length;
                int right_x = startX + (index + 1) * axis_length;


                float range_left = Math.Abs(data[index].data_label[0] - data[index].data_label[data[index].data_label.Count - 1]);
                float range_right = Math.Abs(data[index + 1].data_label[0] - data[index + 1].data_label[data[index + 1].data_label.Count - 1]);


                for (int i = 0; i < data_num; i++)
                {

                    Point p1, p2;
                    Pen pen = new Pen(GetTimeColor(i), data_line_size);

                    if (flag_left)
                    {
                        float factor = ((data[index]._data[i] - data[index].data_label[0]) / range_left);
                        int offset = (int)(axis_height * factor);
                        p1 = new Point(left_x, startY + axis_height - offset);
                    }
                    else
                    {
                        float factor = ((data[index]._data[i] - data[index].data_label[data[index].data_label.Count - 1]) / range_left);
                        int offset = (int)(axis_height * factor);
                        p1 = new Point(left_x, startY + offset);
                    }



                    if (flag_right)
                    {
                        float factor = ((data[index + 1]._data[i] - data[index + 1].data_label[0]) / range_right);
                        int offset = (int)(axis_height * factor);
                        p2 = new Point(right_x, startY + axis_height - offset);
                    }
                    else
                    {
                        float factor = ((data[index + 1]._data[i] - data[index + 1].data_label[data[index + 1].data_label.Count - 1]) / range_right);
                        int offset = (int)(axis_height * factor);
                        p2 = new Point(right_x, startY + offset);
                    }


                    g.DrawLine(pen, p1, p2);




                }
            }
           
        }

        public void DestroyLineData(int index)
        {
            if (index + 1 < class_num)
            {
                startX = margin_left + index * axis_length + 1;
                startY = margin_top;

             
                Brush brush = new SolidBrush(this.BackColor);

                g.FillRectangle(brush,startX, startY, axis_length, axis_height);

            }
        }


        public Color GetTimeColor(int cur)
        {
            float factor = cur / data_num;

            int r=data_old.R-(data_old.R-data_new.R)/(data_num-1)*cur;
            int g=data_old.G-(data_old.G-data_new.G)/(data_num-1)*cur;
            int b=data_old.B-(data_old.B-data_new.B)/(data_num-1)*cur;

            Color newColor = Color.FromArgb(r, g, b);

            return newColor;
        }





        public void SwapData(int indexA,int indexB)
        {

            //Swap(data, indexA, indexB);
            DataLabel l=new DataLabel();
            l.Copy(data[indexA]);
            data[indexA].Copy(data[indexB]);
            data[indexB].Copy(l);


        }

        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public void TestFunc()
        {
            for (int i = 0; i < class_num - 1; i++)
            {
                DestroyLineData(i);
                RedrawAxis(i+1);
            }

       
        }
        public void TestFunc2()
        {
            for (int i = 0; i < class_num - 1; i++)
                DrawDataLine(i);
        }
    }
    public class DataLabel
    {
        public List<float> data_label;
        public Label label;
        public List<float> _data;

        public DataLabel()
        {
            data_label = new List<float>();
            label = new Label();
            _data = new List<float>();
        }

        public void Copy(DataLabel l)
        {
            data_label.Clear();
            foreach (float f in l.data_label)
                data_label.Add(f);
            _data.Clear();
            foreach (float f in l._data)
                _data.Add(f);
            label.Name = l.label.Name;
            label.Text = l.label.Text;
        }



        public void Reverse()
        {
            data_label.Reverse();
        }

        public bool isObj(Label l)
        {
            return l.Name == label.Name;
        }
       
    }
}
