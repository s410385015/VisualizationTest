﻿using System;
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

            dataFile="Plastics_and_Chemicals_Macro.csv";
        }
        
        //Load data from the csv, and fiil in the list of the informations
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

            //TestFunc();
          
        }
       
        //Use by other data processing function
        //eg. ExecutePython.cs
        public void SetByPreprocessData(List<string> _label,List<string> _date,List<List<float>> _data)
        {
            label = _label;
            for (int i = 0; i < _date.Count;i++ )
            {
                Data tmp = new Data( _date[i],_data[i]);
                data.Add(tmp);
            }


            labelNum = label.Count;
            dataNum = data.Count;
        }


        //Calculate the min and max value of the each axis by condition
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
                    LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f*_max), Math.Abs(_min - 0.1f*_min), 2);
                    labelRange.Add(lmm);
                }
                else
                {
                    LabelMinMax lmm = new LabelMinMax(_max, _min , (int)((_max - _min) / ((_maxRange + _minRange) / 2))+2);
                    labelRange.Add(lmm);
                }
                 
                //LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f * _max), Math.Abs(_min - 0.1f * _min), (int)((_max - _min) / ((_maxRange + _minRange) / 2)) + 2);
                //labelRange.Add(lmm);
            }
        }

        //Calculate the min and max value of the each axis by whole data;
        public void CalculateLabelRange()
        {
            labelRange = new List<LabelMinMax>();
            for (int i = 0; i < labelNum; i++)
            {
                float _min, _max, _minRange, _maxRange;
                float tmp = data[0].data[i];
                _max = -0xffffffff;
                _min = _minRange = 0xffffffff;
                _maxRange = 0;


                for (int j = 0; j < data.Count; j++)
                {

                    _max = Math.Max(_max, data[j].data[i]);
                    _min = Math.Min(_min, data[j].data[i]);

                    //_maxRange = Math.Max(_maxRange, Math.Abs(pick_data[j].data[i] - tmp));
                    //_minRange = Math.Min(_minRange, Math.Abs(pick_data[j].data[i] - tmp));

                    tmp = data[j].data[i];
                }


                

                LabelMinMax lmm = new LabelMinMax(_max , _min, 5);
                labelRange.Add(lmm);
            }
        }

        public void TestFunc()
        {
              //CalculateLabelRange(SearchData("2007/1/2", "2007/1/10"));
              foreach (LabelMinMax lmm in labelRange)
                  lmm.Printf();
        }


        //Search for the data that meet the conditions
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
        public List<float> value;
        public float i;
        public LabelMinMax(float x,float n,int r)
        {
            min=n;
            max=x;
            rangeBetween = r;

            i = (max - min) / r;

            value = new List<float>();

            for (int j = -1; j <= r+1; j++)
                value.Add(min + (i * j));
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
