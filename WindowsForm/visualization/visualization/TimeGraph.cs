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
    
    public partial class TimeGraph : UserControl
    {
        private int margin_top;
        private int margin_bottom;
        private int margin_left;
        private int margin_right;
        private Graphics g;
        private Color back_line_color;
        private Color curve_line_color;
        private int back_line_size;
        private int pic_width;
        private int pic_height;
        private Font axis_label_font;
        private StringFormat axis_label_format;
        private List<Point> data_point;
        private float max_value;
        private int startX, startY;
        private int endX, endY;
        public delegate void rightClickEvent(object sender, MouseEventArgs e);
        public rightClickEvent rce;
        public List<float> a;
        public List<float> b;
        public List<string> ith; 
        public string a_label;
        public string b_label;
        
        public Point cur_pos;
        public int select_index;
        public int select_index_start;
        public Color ray_color;
        public matrix FuncMatrix;
        public bool useRegression;
        public float co;
        public float bias;
        public bool isMouse;
        public Point start_pos;
        public delegate void CallBack(string a,string b);
        public CallBack cb;
        public bool flag;
        public List<Data> draw_data;
        public int total;
     
        public Dictionary<int, bool> DataShown;

        public TimeGraph()
        {
            InitializeComponent();
            Init();
            
        }

        private void TimeGraph_Load(object sender, EventArgs e)
        {
            pic.Location = new Point(0, 0);
            
            
        }

        
        public void Init()
        {
            margin_bottom = 20;
            margin_top = 20;
            margin_left = 60;
            margin_right = 80;
            back_line_color = Color.Black;
            curve_line_color = Color.Blue;
            back_line_size = 1;

            pic.Size = this.Size;

            pic_width = this.Width;
            pic_height = this.Height;

            axis_label_font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            axis_label_format = new StringFormat();
            axis_label_format.Alignment = StringAlignment.Center;
            data_point = new List<Point>();
            max_value = 0;
            cur_pos = new Point(0, 0);
            ray_color = Color.Red;
            a_label = "";
            b_label = "";
            co = 1f;
            bias = 0;
            a = new List<float>();
            b = new List<float>();
            ith = new List<string>();
            useRegression = true;
            isMouse = false;
            flag = false;
            draw_data = new List<Data>();
            total = 0;
           
            DataShown = new Dictionary<int, bool>();
        }


        private void pic_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Update();
        }

        public void NotifyRedraw()
        {
           
            pic.Invalidate();
        }

        private void Update()
        {
            pic.Size = this.Size;
            pic_width = this.Width;
            pic_height = this.Height;

            startX = margin_left;
            startY = margin_top;
            endX = pic_width - margin_right;
            endY = pic_height - margin_bottom;

            for (int i = 0; i < data_point.Count; i++)
            {
                if (data_point[i].X == cur_pos.X)
                    select_index = i;
                if (data_point[i].X == start_pos.X)
                    select_index_start = i;
            }

            DrawAxis();
            DrawData();
            DrawRay();
            DrawBoundary();


           
            for(int i=0;i<draw_data.Count;i++)
            {
                        
                if(DataShown.ContainsKey(draw_data[i].type)&&DataShown[draw_data[i].type])
                    DrawDataByList(draw_data[i].p,draw_data[i].dataColor);
                
            }

            for (int i = 0; i < draw_data.Count; i++)
            {
                if (draw_data[i].type == 5)
                {
                    if (DataShown.ContainsKey(draw_data[i].type) && DataShown[draw_data[i].type])
                        DrawRayByList(i);
                }

            }
        }
        
        public void reInit()
        {

        }

        public void DrawAxis()
        {
          
            g.DrawLine(new Pen(back_line_color,back_line_size),new Point(startX,startY),new Point(startX,endY));
            g.DrawLine(new Pen(back_line_color, back_line_size), new Point(startX, (startY + endY) / 2), new Point(endX, (startY + endY) / 2));

            g.DrawLine(new Pen(back_line_color, back_line_size), new Point(startX-3, startY), new Point(startX+3, startY));
            g.DrawLine(new Pen(back_line_color, back_line_size), new Point(startX - 3, endY), new Point(startX + 3, endY));


            g.DrawString("Diff", 
                axis_label_font, 
                new SolidBrush(back_line_color),
                new Rectangle(new Point(startX-50,startY-10),new Size(60,40)) ,
                axis_label_format);

            g.DrawString("Time",
                axis_label_font,
                new SolidBrush(back_line_color),
                new Rectangle(new Point(endX  ,(startY+endY)/2), new Size(60, 40)),
                axis_label_format);


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;

            g.DrawString(max_value.ToString("0.00"),
                axis_label_font,
                new SolidBrush(back_line_color),
                new Rectangle(new Point(startX+10,startY-10),new Size(100,40)),
                sf);
            g.DrawString("- " + max_value.ToString("0.00"),
               axis_label_font,
               new SolidBrush(back_line_color),
               new Rectangle(new Point(startX+10 , endY), new Size(100, 40)),
               sf);
        }

        public void SetDrawData(List<float> d,int t,int o,Color c,timeGraphObj _obj)
        {
            float per_width = (endX-startX ) / (float)total;
            int per_length = (endY - startY) / 2;

            //float max = timeGraphObj.GetMax(d);
            float max = max_value;

            List<Point> tmpP = new List<Point>();

            for (int i = 0; i < d.Count; i++)
                tmpP.Add(new Point((int)(startX + per_width * (i+o)), (int)((endY + startY) / 2 - per_length * (d[i] / max))));


            Data _data=new Data(t,o,per_width,tmpP,d,c,_obj);
            draw_data.Add(_data);

            


            /*
            float per_width = (endX - startX) / (float)data.Count;
            data_point.Clear();
            int per_length = (endY - startY) / 2;

            for (int i = 0; i < data.Count; i++)
                data_point.Add(new Point((int)(startX + per_width * i), (int)((endY + startY) / 2 - per_length * (data[i] / max))));
        
             */ 
        }

        private void DrawDataByList(List<Point> p,Color c)
        {

            if (p.Count > 0)
            {
                Point[] points = p.ToArray();
                g.DrawCurve(new Pen(c, back_line_size), points);

            }
        }


        public void SetData(List<float> _a,List<float> _b,string al,string bl,List<string> _ith)
        {

          
            
            a = _a;
            b = _b;
            ith = _ith;
            CalculateRegression();
            SetUseRegression(true);
          

            List<float> diff = new List<float>();
            float max = 0;
            for (int i = 0; i < a.Count; i++)
            {
                max = Math.Max(Math.Abs(a[i]*co+bias - b[i]), max);
                diff.Add(a[i] * co + bias - b[i]);
            }
            max_value = max;
            a_label = al;
            b_label = bl;
            total = diff.Count;

            ExtractDataPoint(diff, max);
            NotifyRedraw();
            flag = true;
        } 


        public void Reset()
        {
            pic.Size = this.Size;
            pic_width = this.Width;
            pic_height = this.Height;

            startX = margin_left;
            startY = margin_top;
            endX = pic_width - margin_right;
            endY = pic_height - margin_bottom;


           

            List<float> diff = new List<float>();
            float max = 0;
            for (int i = 0; i < a.Count; i++)
            {
                max = Math.Max(Math.Abs(a[i]*co+bias - b[i]), max);
                diff.Add(a[i]*co+bias - b[i]);
            }
            max_value = max;
            ExtractDataPoint(diff, max);
        }

        public void ExtractDataPoint(List<float> data,float max)
        {

            float per_width = (endX-startX ) / (float)data.Count;
            data_point.Clear();
            int per_length = (endY - startY)/2;

            for(int i=0;i<data.Count;i++)
                data_point.Add(new Point((int)(startX+per_width*i),(int)((endY + startY)/2-per_length*(data[i]/max))));
           
        }

        private void DrawData()
        {
           
            if (data_point.Count > 0)
            {
                Point[] points = data_point.ToArray();
                g.DrawCurve(new Pen(curve_line_color, back_line_size), points);
        
            }
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {

                        rce(sender, e);
                    }
                    break;
                case MouseButtons.Left:
                    {
                        isMouse = true;
                        start_pos = e.Location;
                    }
                    break;
  
            }
        }

        public void DrawRay()
        {
            if (data_point.Count > 0&&!isMouse)
            {
                

                g.DrawLine(new Pen(ray_color, back_line_size), new Point(cur_pos.X, startY), new Point(cur_pos.X, endY));
                string s = "(" + ith[select_index]
                    + ", " + a_label + ":" + a[select_index].ToString("0.00")
                    + ", " + b_label + ":" + b[select_index].ToString("0.00") 
                    + ", k = "+(a[select_index]*co-b[select_index]).ToString("0.00");


                if (useRegression)
                    s += ", m = " + FuncMatrix.m[0].ToString("0.00") + ", b = " + FuncMatrix.m[1].ToString("0.00");

                s += ")";
           
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;

                g.DrawString(s,
                   axis_label_font,
                   new SolidBrush(ray_color),
                   new Rectangle(new Point(cur_pos.X, cur_pos.Y-20), new Size(s.Length * 9, 40)),
                   sf);



            }
        }


        public void DrawRayByList(int i)
        {
            if (data_point.Count > 0 && !isMouse)
            {


                //g.DrawLine(new Pen(ray_color, back_line_size), new Point(cur_pos.X, startY), new Point(cur_pos.X, endY));
                string s="( a = "+draw_data[i].obj.a+" , b = "+draw_data[i].obj.b+" )";
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;

                g.DrawString(s,
                   axis_label_font,
                   new SolidBrush(draw_data[i].dataColor),
                   new Rectangle(new Point(cur_pos.X, cur_pos.Y - 40), new Size(s.Length * 9, 40)),
                   sf);



            }
        }



        private void pic_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            cur_pos = e.Location;
            cur_pos.X = Math.Max(cur_pos.X, margin_left);
            cur_pos.X = Math.Min(cur_pos.X, this.Width - margin_right);
            cur_pos.Y = Math.Max(cur_pos.Y, margin_top);
            cur_pos.Y = Math.Min(cur_pos.Y, this.Height - margin_bottom);
            NotifyRedraw();


            
        }
        public void Flip()
        {
       
            Swap(ref a_label,ref b_label);
            Swap(ref a, ref b);
            int per_length = (endY - startY)/2;
            for(int i=0;i<data_point.Count;i++)
            {
                
                int y=(int)((endY + startY)/2-per_length*((a[i]-b[i])/max_value));
                data_point[i]=new Point(data_point[i].X,y);
            }
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public void CalculateRegression()
        {
            List<float> _a = new List<float>();
            for (int i = 0; i < a.Count; i++)
            {
                _a.Add(a[i]);
                _a.Add(1);
            }
            matrix ma = new matrix(a.Count, 2, _a);
            matrix mb = new matrix(b.Count, 1, b);
            matrix mata = matrix.Mul(ma.Transpose(), ma);
            mata = mata.Invert();

            FuncMatrix = matrix.Mul(matrix.Mul(mata, ma.Transpose()), mb);
            
        }
        public void SetUseRegression(bool b)
        {
            useRegression = b;
            if (b)
            {
                bias = FuncMatrix.m[1];
                co = FuncMatrix.m[0];
            }
            else
            {
                co = 1f;
                bias = 0;
            }
            Reset();
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
              
                case MouseButtons.Left:
                    {
                        isMouse = false;
                        if(flag)
                            cb(ith[select_index_start],ith[select_index]);
                    }
                    break;

            }
        }

        public void DrawBoundary()
        {
            if(isMouse)
            {
                Point p=new Point(Math.Min(start_pos.X,cur_pos.X),Math.Min(start_pos.Y,cur_pos.Y));
                Rectangle rec=new Rectangle(p,new Size(Math.Abs(start_pos.X-cur_pos.X),Math.Abs(start_pos.Y-cur_pos.Y)));
                g.DrawRectangle(new Pen(ray_color, back_line_size), rec);
                
            }
        }

        private void pic_Click(object sender, EventArgs e)
        {

        }

        public void AddShown(int index,bool flag=true)
        {
            if (!DataShown.ContainsKey(index))
                DataShown.Add(index, true);
            else
                DataShown[index] = flag;
        }

       

        public class Data
        {
            public int type;
            public int offset;
            public Color dataColor;
            public float perWidth;
            public List<float> _data;
            public List<Point> p;
            public timeGraphObj obj;
            public Data()
            {
                 type = -1;
                 offset = 0;
                 perWidth = 0;
                 _data = new List<float>();
                 p = new List<Point>();
                 dataColor = Color.Red;
            }
            public Data(int t,int o,float per,List<Point> _p,List<float> d,Color c,timeGraphObj _obj)
            {
                type = t;
                offset = o;
                perWidth = per;
                p = new List<Point>();
                p = _p;
                _data = new List<float>();
                _data = d;
                dataColor = c;
                obj = _obj;
            }
        }
    }
}
