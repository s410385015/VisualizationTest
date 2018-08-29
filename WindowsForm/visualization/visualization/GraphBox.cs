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
        public float axis_size;
        public float line_size;
        public int label_axis_size;
        public int label_axis_width;
        public int label_size;

        public int class_num;
        public int label_num;

        public Graphics g;
        public int label_height;
        public Color axis_color;
        
        public Graph()
        {
            InitializeComponent();
           
           
        }

        

        public void Init()
        {
            margin_top = 10;
            margin_bottom = 10;
            margin_left = 20;
            margin_right = 10;

            axis_size = 3;
            axis_color = Color.Black;
            
            
            line_size = 3;

            label_axis_size = 3;
            label_axis_width = 5;

            label_size = 5;
            
            label_num = 10;
            class_num = 3;
            label_height = 20;
            
        }

        public void GraphInit()
        {
            g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawRectangle(new Pen(axis_color, axis_size), new Rectangle(50, 50, 100, 100));
        }

        public void DrawAxis()
        {
            int w = this.Size.Width;
            int h = this.Size.Height;

            //axis length between each others
            int axis_length = (w - margin_left - margin_right)/(class_num-1);
            int axis_height = this.Size.Height - margin_top - margin_bottom - label_height;
            

            //axis label length between each others
            int axis_label_length = axis_height / (label_num+1);


            int startX, startY;

            startX = margin_left;
            startY = margin_top;
            Pen pen = new Pen(axis_color, axis_size);
            for(int i=0;i<class_num;i++)
            {
                
                Point p1=new Point(startX+i*axis_length,startY);
                Point p2=new Point(startX+i*axis_length,startY+axis_height);

                for (int j = 1; j <= label_num;j++ )
                {
                    Point p3 = new Point(p1.X -label_axis_width, startY+j*axis_label_length);
                    Point p4 = new Point(p2.X, startY + j * axis_label_length);
                    g.DrawLine(pen, p3, p4);
                }    
                g.DrawLine(pen, p1, p2);
            }


        }

        private void Graph_Load(object sender, EventArgs e)
        {

            Init();
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
    }
}
