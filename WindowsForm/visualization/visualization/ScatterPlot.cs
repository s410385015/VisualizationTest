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
    public partial class ScatterPlot : UserControl
    {
        private Graphics g;
        private Color BackLineColor;
        private int box_line_size;

        private int padding_top;
        private int padding_bottom;
        private int padding_left;
        private int padding_right;
        private int index=1;
        int per_height;
        int per_width;
        int startX, startY, endX, endY;

        private float box_horizontal_factor;
        private float box_vertical_facotr;
        private float padding_box_bottom;
        private int label_axis_length;
        private int label_height;
        private int label_width;
        private List<Label> labels;
        private Font label_font;
        private Font category_font;
        private Font cc_font;
        private int cc_size;
        private SolidBrush cc_brush;
        private int category_size;
        private SolidBrush category_brush;
        private SolidBrush data_brush;
        private StringFormat category_format;
        private int data_size;
        private List<List<float>> label_data;
        private List<LabelInfo> label_info;
        public bool isExist = false;
        public ScatterPlot()
        {
            InitializeComponent();
            
        }

        private void ScatterPlot_Paint(object sender, PaintEventArgs e)
        {
            
        }

        public void Init(int i=1)
        {
            BackLineColor = Color.Black;

            padding_top = 50;
            padding_bottom = 50;
            padding_left = 50;
            padding_right = 50;

            box_line_size = 1;
            index = i;

            pic.Size = new Size(this.Width, this.Height);
            pic.Location = new Point(0, 0);

            box_horizontal_factor = 0.12f;
            box_vertical_facotr = 0.1f;

            startX = padding_left;
            startY = padding_top;

            endX = this.Width - padding_right;
            endY = this.Height - padding_bottom;

            per_height = (int)Math.Round((float)((endY - startY) / index));
            per_width = (int)Math.Round((float)(endX - startX) / index);

            Label l = new Label();
            label_axis_length = 5;
            label_height = l.Height;
            label_width = (int)(l.Width/2);
            labels = new List<Label>();
            label_font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            category_size = 15;
            category_font = new Font("Arial", category_size);
            category_brush = new SolidBrush(Color.Black);
            category_format = new StringFormat();

            data_brush = new SolidBrush(Color.Blue);
            data_size = 5;
            label_data = new List<List<float>>();
            label_info = new List<LabelInfo>();

            cc_size=10;
            cc_font = new Font("Arial", cc_size);
            cc_brush = new SolidBrush(Color.Red);
           
        }

        public void SetLabelData(List<List<float>> d,List<string> name)
        {
            label_data = d;

            for(int i=0;i<name.Count;i++)
            {
                LabelInfo lf = new LabelInfo();
                lf.Set(name[i], label_data[i]);
                label_info.Add(lf);
            }
            Console.WriteLine("");
        }


        public void DrawBox()
        {


            for (int i = 0; i < index; i++)
            {
                for (int j = 0; j < index; j++)
                {
                    if (i != j)
                    {
                        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(startX + per_width * i, startY + per_height * j, per_width, per_height));
                        
                        if (i == 0)
                        {
                            DrawLabel(startX , startY + per_height * j, 1,j);

                        }
                        if(i==index-1)
                        {
                            DrawLabel(startX + per_width * (i+1), startY + per_height * j, 3,j);
                        }
                        if (j == 0){
                           DrawLabel(startX + per_width * i, startY , 0,i);
                           
                        }
                        if(j==index-1)
                        {
                            DrawLabel(startX + per_width * i, startY + per_height * (j+1), 2,i);
                        }


                        for (int k = 0; k < label_data[i].Count;k++ )
                        {
                                DrawData(label_info[i], label_info[j], label_data[i][k], label_data[j][k], new Point(startX + per_width * i + (int)(per_width * box_horizontal_factor - data_size / 2), startY + per_height * (j + 1) - (int)(per_height * box_vertical_facotr + data_size / 2)));
                        }

                        CorrelationCoefficient cc = new CorrelationCoefficient(label_data[i], label_data[j]);
                        DrawCCText(cc.CalculateCC().ToString("0.00"), new Point(startX + per_width * i, startY + per_height * j));
                    
                    }
                    else
                    {
                        GenerateLabel(new Point(startX + per_width * i+1 , startY + per_height * j+1 ), label_info[i].label_name, ContentAlignment.MiddleCenter);
                    }
                    
                }
            }

            
            //g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(startX, startY), new Point(endX, endY));
                        
            for(int i=0;i<=index;i++)
            {
                g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(startX, startY + per_height * i), new Point(endX, startY+per_height * i));
                g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(startX + per_width * i, startY), new Point(startX + per_width * i, endY));

            }

            
           
        }

        public void DrawData(LabelInfo lx,LabelInfo ly,float x,float y,Point p)
        {

            float fx=lx.CalculateFactor(x);
            float fy = ly.CalculateFactor(y);
            int _x=(int)Math.Round(per_width*(1-box_horizontal_factor*2)*fx);
            int _y=(int)Math.Round(per_height*(1-box_vertical_facotr*2)*fy);

            g.FillEllipse(data_brush,p.X+_x,p.Y-_y,data_size,data_size);
        }


        public void DrawCCText(string s,Point p)
        {
            g.DrawString(s, cc_font, cc_brush, p);
        }



        //flag == true  horizontal
        //flag == false vertical

        public void DrawLabel(int x, int y,int type, int index)
        {
                LabelInfo li = label_info[index];
               if(type==0)
               {

                   int offset=0;
                   
                   //if(index%2==1)
                        offset= label_axis_length + label_height;
                   //else
                       //offset = label_axis_length + label_height*2;
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x + (int)(per_width * box_horizontal_factor), y), new Point(x + (int)(per_width * box_horizontal_factor), y - label_axis_length));
                   GenerateLabel(new Point(x + (int)(per_width * box_horizontal_factor - label_width / 2), y-offset), li.min,ContentAlignment.MiddleCenter);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x + (int)(per_width * (1 - box_horizontal_factor)), y), new Point(x + (int)(per_width * (1 - box_horizontal_factor)), y - label_axis_length));
                   GenerateLabel(new Point(x + (int)(per_width * (1 - box_horizontal_factor) - label_width / 2), y -offset), li.max, ContentAlignment.MiddleCenter);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point((int)(x + per_width / 2), y), new Point((int)(x + per_width / 2), y - label_axis_length));
                   GenerateLabel(new Point((int)(x + per_width / 2) - label_width / 2, y -offset), (li.max + li.min) / 2, ContentAlignment.MiddleCenter);
               }
               else if(type==1)
               {
                   int offset = label_axis_length + label_width;
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x - label_axis_length, y + (int)(per_height * box_vertical_facotr)), new Point(x, y + (int)(per_height * box_vertical_facotr)));
                   GenerateLabel(new Point(x - offset, y + (int)(per_height * box_vertical_facotr - label_height / 2)), li.max, ContentAlignment.MiddleRight);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x - label_axis_length, y + (int)(per_height * (1 - box_vertical_facotr))), new Point(x, y + (int)(per_height * (1 - box_vertical_facotr))));
                   GenerateLabel(new Point(x - offset, y + (int)(per_height * (1 - box_vertical_facotr) - label_height / 2)), li.min, ContentAlignment.MiddleRight);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x - label_axis_length, y + (int)(per_height / 2)), new Point(x, y + (int)(per_height / 2)));
                   GenerateLabel(new Point(x - offset, y + (int)(per_height / 2 - label_height / 2)), (li.max + li.min) / 2, ContentAlignment.MiddleRight);
               }
               else if(type==2)
               {
                   int offset = 0;

                   //if (index % 2 == 1)
                       offset = label_axis_length;
                   //else
                       //offset = label_axis_length + label_height;
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x + (int)(per_width * box_horizontal_factor), y), new Point(x + (int)(per_width * box_horizontal_factor), y + label_axis_length));
                   GenerateLabel(new Point(x + (int)(per_width * box_horizontal_factor - label_width / 2), y + offset), li.min, ContentAlignment.MiddleCenter);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x + (int)(per_width * (1 - box_horizontal_factor)), y), new Point(x + (int)(per_width * (1 - box_horizontal_factor)), y + label_axis_length));
                   
                   GenerateLabel(new Point(x + (int)(per_width * (1 - box_horizontal_factor) - label_width / 2), y + offset), li.max, ContentAlignment.MiddleCenter);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point((int)(x + per_width / 2), y), new Point((int)(x + per_width / 2), y + label_axis_length));
                   GenerateLabel(new Point((int)(x + per_width / 2) - label_width / 2, y + offset), (li.max + li.min) / 2, ContentAlignment.MiddleCenter);
               }
               else
               {
                   int offset = label_axis_length;
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x + label_axis_length, y + (int)(per_height * box_vertical_facotr)), new Point(x, y + (int)(per_height * box_vertical_facotr)));
                   GenerateLabel(new Point(x + offset, y + (int)(per_height * box_vertical_facotr - label_height / 2)), li.max, ContentAlignment.MiddleLeft);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x + label_axis_length, y + (int)(per_height * (1 - box_vertical_facotr))), new Point(x, y + (int)(per_height * (1 - box_vertical_facotr))));
                   GenerateLabel(new Point(x + offset, y + (int)(per_height * (1 - box_vertical_facotr) - label_height / 2)), li.min, ContentAlignment.MiddleLeft);
                   g.DrawLine(new Pen(BackLineColor, box_line_size), new Point(x + label_axis_length, y + (int)(per_height / 2)), new Point(x, y + (int)(per_height / 2)));
                   GenerateLabel(new Point(x + offset, y + (int)(per_height / 2 - label_height / 2)), (li.max + li.min) / 2, ContentAlignment.MiddleLeft);
               }
           
        }


        public void NotifyRedraw()
        {
            pic.Invalidate();
        }

        private void ScatterPlot_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isExist)
            {
   
                DrawBox();
            }
            
        }
        public void GenerateLabel(Point p, float value,ContentAlignment ca)
        {

            Label label = new Label();
            label.TextAlign = ca;
            label.Font = label_font;
            label.Location = p;
            label.Size=new Size((int)(label.Width/2),label.Height);
            //label.BackColor = Color.Pink;
            //label.Size = new Size(label_width, label_height);

            label.Text = Math.Round(value).ToString();
           
            pic.Controls.Add(label);
            labels.Add(label);
        }

        public void GenerateLabel(Point p, string value, ContentAlignment ca)
        {

            Label label = new Label();
            label.Size = new Size(per_width-1, per_height-1);
            label.TextAlign = ca;
            label.Font = category_font;
            label.Location = p;
            //label.BackColor = Color.Pink;
            //label.Size = new Size(label_width, label_height);

            label.Text = value;

            pic.Controls.Add(label);
            labels.Add(label);
        }

        public void Rest()
        {
            foreach (Label l in labels)
            {
                pic.Controls.Remove(l);
              
            }
            labels.Clear();
            label_data = new List<List<float>>();
            label_info = new List<LabelInfo>();
        }

        public class LabelInfo
        {
            public float min;
            public float max;
            public string label_name;

            public LabelInfo()
            {
                min = max = 0;
                label_name = "";
            }

            public LabelInfo(float n,float x,string ln)
            {
                min = n;
                max = x;
                label_name = ln;
            }

            public void Set(string s, List<float> data)
            {
                label_name = s;


                float _min, _max;

                _min = 0xffff;
                _max =-0xffff;

                foreach(float f in data)
                {
                    _max = Math.Max(f, _max);
                    _min = Math.Min(f, _min);
                }

                max = _max*1.1f;
                min = _min*0.9f;

            }

            public float CalculateFactor(float f)
            {

                return ((f - min) / (max - min));
            }


            
            

        }
    }
    
}
