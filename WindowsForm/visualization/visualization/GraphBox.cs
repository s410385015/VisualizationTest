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
        public int margin_top;
        public int margin_bottom;
        public int margin_left;
        public int margin_right;

        public Color axis_color;
        public float axis_size;

        public float line_size;


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
        
        
        
        public int tmp_select;
        public List<DataLabel> data;

        public Graphics g;
        
        


        public Graph()
        {
            InitializeComponent();
           
           
        }

        

        public void Init()
        {
            tmp_select = -1;


            margin_top = 20;
            margin_bottom = 10;
            margin_left = 50;
            margin_right = 50;

            axis_size = 3;
            axis_color = Color.Black;
            
            
            line_size = 3;

            label_axis_size = 3;
            label_axis_width = 5;
            label_axis_num = 10;
            label_axis_sizeOflabel = 8;
            label_axis_heightOflabel = 15;
            label_axis_widthOflabel = 20;
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
           

            label_color_org = Color.Black;
            label_color_select = Color.Red;
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
            int w = this.Size.Width;
            int h = this.Size.Height;

            //axis length between each others
            int axis_length = (w - margin_left - margin_right)/(class_num-1);
            int axis_height = this.Size.Height - margin_top - margin_bottom - label_height;
      


            int startX, startY;

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
            DrawAxis();
        }

        public void GenerateLabel(Point p,int index)
        {

            data[index].label = new Label();
            data[index].label.Font = label_font;
            data[index].label.Location = p;
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
                        RedrawAxis(i);
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




                            SwapData(i, tmp_select);


                            RedrawAxis(i);
                            RedrawAxis(tmp_select);



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
            int w = this.Size.Width;
            int h = this.Size.Height;

            //axis length between each others
            int axis_length = (w - margin_left - margin_right) / (class_num - 1);
            int axis_height = this.Size.Height - margin_top - margin_bottom - label_height;



            int startX, startY;

            startX = margin_left + index * axis_length;
            startY = margin_top;

            Brush brush = new SolidBrush(this.BackColor);

            //Console.WriteLine((startX - label_axis_widthOflabel * 2) + " " + (startY - 1) + " " + (label_axis_widthOflabel * 4) + " " + (startY + axis_height + 1));
            g.FillRectangle(brush, startX - label_axis_widthOflabel * 2, startY - label_axis_heightOflabel, label_axis_widthOflabel * 4, startY + axis_height + 1);
            //g.DrawRectangle(pen, startX - label_axis_widthOflabel * 2, startY - 1, label_axis_widthOflabel * 4, startY + axis_height + 1);
          
        }

        public void DisSelectLabel(int indexA,int indexB)
        {
            Console.WriteLine("??????????");
           
            data[indexA].label.ForeColor = label_color_org;
            data[indexB].label.ForeColor = label_color_org;
            

           
            
        }



        public void RedrawAxis(int index)
        {

         
            //true : bottom to top
            //false: up to bottom
            bool flag = true;

            if (data[index].data_label.Count > 1)
                flag = (data[index].data_label[1] > data[index].data_label[0]) ? (true) : (false);

            int w = this.Size.Width;
            int h = this.Size.Height;

            //axis length between each others
            int axis_length = (w - margin_left - margin_right) / (class_num - 1);
            int axis_height = this.Size.Height - margin_top - margin_bottom - label_height;



            int startX, startY;

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
            data[1].label.ForeColor = Color.Red;
       
        }
        public void TestFunc2()
        {
            data[1].label.ForeColor = Color.Black; 
        }
    }
    public class DataLabel
    {
        public List<float> data_label;
        public Label label;


        public DataLabel()
        {
            data_label = new List<float>();
            label = new Label();

        }

        public void Copy(DataLabel l)
        {
            data_label.Clear();
            foreach (float f in l.data_label)
                data_label.Add(f);
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
