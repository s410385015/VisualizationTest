using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace visualization
{

    public class DataProcessing
    {
        public string data_name;
        public bool useDB;
        public string dataFile;
        public List<string> label;
        public List<LabelMinMax> labelRange;
        public List<Data> data;
        public int labelNum;
        public int dataNum;


        public DataProcessing()
        {
            label=new List<string>();
            data=new List<Data>();

            dataFile="D:\\桌面用\\Plastics_and_Chemicals_Macro.csv";
        }
        
        public void LoadData()
        {
            string[] raw_data = File.ReadAllLines (dataFile,System.Text.Encoding.Default);

           

            string[] raw_label = raw_data[0].Split(',');
            for (int i = 1; i < raw_label.Length;i++)
                label.Add(raw_label[i]);
           

            for(int i=1;i<raw_data.Length;i++)
            {
                string[] raw = raw_data[i].Split(',');
                List<float> d = new List<float>();

                for (int j = 1; j < raw.Length; j++)
                {
                    try
                    {
                        d.Add(float.Parse(raw[j]));
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(i + "-" + j + "-" + e.ToString());
                    }
                }

                Data tmp = new Data(raw[0], d);
                data.Add(tmp);

             
            }


            labelNum = label.Count;
            dataNum = data.Count;

            TestFunc();
          
        }


        public void CalculateLabelRange(List<Data> pick_data)
        {
            labelRange = new List<LabelMinMax>();
            for(int i=0;i<labelNum;i++)
            {
                float _min, _max, _minRange, _maxRange;
                float tmp = pick_data[0].data[i];
                _max = -0xffffffff;
                _min=_minRange = 0xffffffff;
                _maxRange=0;
                

                for(int j=0;j<pick_data.Count;j++)
                {
                   
                    _max = Math.Max(_max, pick_data[j].data[i]);
                    _min = Math.Min(_min, pick_data[j].data[i]);

                    _maxRange = Math.Max(_maxRange, Math.Abs(pick_data[j].data[i]-tmp));
                    _minRange = Math.Min(_minRange, Math.Abs(pick_data[j].data[i] - tmp));

                    tmp = pick_data[j].data[i];
                }

                if(_min==_max)
                {
                    LabelMinMax lmm = new LabelMinMax(_max + 0.1f*_max, _min - 0.1f*_min, 2);
                    labelRange.Add(lmm);
                }
                else
                {
                    LabelMinMax lmm = new LabelMinMax(_max + _minRange, _min - _minRange, (int)((_max - _min) / ((_maxRange + _minRange) / 2))+2);
                    labelRange.Add(lmm);
                }
                
            }
        }


        public void TestFunc()
        {
              CalculateLabelRange(SearchData("2007/1/2", "2007/1/10"));
              foreach (LabelMinMax lmm in labelRange)
                  lmm.Printf();
        }

        public List<Data> SearchData(string d1,string d2)
        {
           
            Data tmp1 = new Data(d1);
            Data tmp2 = new Data(d2);

            List<Data> tmp = new List<Data>();

            foreach(Data d in data)
            {
                if (d > tmp2)
                    break;

                if (d >= tmp1)
                    tmp.Add(d);
            }

            return tmp;
        }


        
      
    }
    



    public class LabelMinMax
    {
        public float min;
        public float max;
        public int rangeBetween;
        public LabelMinMax(float n,float x,int r)
        {
            min=n;
            max=x;
            rangeBetween = r;

        }

        public void Printf()
        {
            Console.WriteLine("Max: " + max + " - Min: " + min + " - Range: " + rangeBetween);
        }
    }

    public class Data
    {
        public string time;
        public List<float> data;

        public Data(string t,List<float> d)
        {
            time = t;
            data = d;
        }

        public Data(string t)
        {
            time = t;
        }
       
        public void Print()
        {
            Console.WriteLine("");
            Console.WriteLine(time);

            foreach (float f in data)
                Console.Write(f + "-");
        }

        
        public static bool operator >(Data d1,Data d2)
        {
            int t1, t2;
            string[] s1, s2;
            s1=d1.time.Split('/');
            s2=d2.time.Split('/');

            t1 = (Int32.Parse(s1[0]) * 365) + (Int32.Parse(s1[1]) * 31) + (Int32.Parse(s1[2]));
            t2 = (Int32.Parse(s2[0]) * 365) + (Int32.Parse(s2[1]) * 31) + (Int32.Parse(s2[2]));
            return t1 > t2;
        }

        public static bool operator <(Data d1, Data d2)
        {
            int t1, t2;
            string[] s1, s2;
            s1 = d1.time.Split('/');
            s2 = d2.time.Split('/');

            t1 = (Int32.Parse(s1[0]) * 365) + (Int32.Parse(s1[1]) * 31) + (Int32.Parse(s1[2]));
            t2 = (Int32.Parse(s2[0]) * 365) + (Int32.Parse(s2[1]) * 31) + (Int32.Parse(s2[2]));
            return t1 < t2;
        }
        public static bool operator >=(Data d1, Data d2)
        {
            int t1, t2;
            string[] s1, s2;
            s1 = d1.time.Split('/');
            s2 = d2.time.Split('/');

            t1 = (Int32.Parse(s1[0]) * 365) + (Int32.Parse(s1[1]) * 31) + (Int32.Parse(s1[2]));
            t2 = (Int32.Parse(s2[0]) * 365) + (Int32.Parse(s2[1]) * 31) + (Int32.Parse(s2[2]));
            return t1 >= t2;
        }
        public static bool operator <=(Data d1, Data d2)
        {
            int t1, t2;
            string[] s1, s2;
            s1 = d1.time.Split('/');
            s2 = d2.time.Split('/');

            t1 = (Int32.Parse(s1[0]) * 365) + (Int32.Parse(s1[1]) * 31) + (Int32.Parse(s1[2]));
            t2 = (Int32.Parse(s2[0]) * 365) + (Int32.Parse(s2[1]) * 31) + (Int32.Parse(s2[2]));
            return t1 <= t2;
        }

    }
}
