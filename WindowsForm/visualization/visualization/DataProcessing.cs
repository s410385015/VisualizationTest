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
                    //LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f*_max), Math.Abs(_min - 0.1f*_min), 2);
                    LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f * _max), Math.Abs(_min - 0.1f * _min), 2);
                    labelRange.Add(lmm);
                }
                else
                {
                    //LabelMinMax lmm = new LabelMinMax(_max, _min , Math.Min((int)((_max - _min) / ((_maxRange + _minRange) / 2))+2,10));
                    LabelMinMax lmm = new LabelMinMax(_max+_minRange, _min-_minRange, Math.Min((int)((_max - _min) / ((_maxRange + _minRange) / 2)) + 2, 5),false);
                    labelRange.Add(lmm);
                }
                 
                //LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f * _max), Math.Abs(_min - 0.1f * _min), (int)((_max - _min) / ((_maxRange + _minRange) / 2)) + 2);
                //labelRange.Add(lmm);
            }
        }

        public void CalculateLabelRange(List<Data> pick_data,List<int> index,List<float> w)
        {
            labelRange = new List<LabelMinMax>();
            for (int i = 0; i < labelNum; i++)
            {
                LabelMinMax lmm = new LabelMinMax();
                labelRange.Add(lmm);
            }
    


            int indexA, indexB;
            indexA = index[0];
            indexB = index[1];
            float _minA, _maxA, _minRangeA, _maxRangeA;
            float _minB, _maxB, _minRangeB, _maxRangeB;
            float _minTmp, _maxTmp, _minRangeTmp, _maxRangeTmp;
            _maxA = -0xffffffff;
            _minA = _minRangeA = 0xffffffff;
            _maxRangeA = 0;

            _maxB = -0xffffffff;
            _minB = _minRangeB = 0xffffffff;
            _maxRangeB = 0;

            _maxTmp = -0xffffffff;
            _minTmp = _minRangeTmp = 0xffffffff;
            _maxRangeTmp = 0;
            float tmpA = pick_data[0].data[indexA];
            float tmpB = pick_data[0].data[indexB];
 
            for (int j = 0; j < pick_data.Count; j++)
            {

                //if (pick_data[j].data[indexA] * w[0] + w[1] - pick_data[j].data[indexB] < 1) ;

                //Console.WriteLine(pick_data[j].data[indexA] + " " + pick_data[j].data[indexB] + " " + w[0] + " " + w[1]);
                //Console.WriteLine((pick_data[j].data[indexA] * w[0] + w[1] - pick_data[j].data[indexB]));
                 _maxA = Math.Max(_maxA, pick_data[j].data[indexA]);
                 _minA = Math.Min(_minA, pick_data[j].data[indexA]);

                 _maxTmp = Math.Max(_maxTmp, pick_data[j].data[indexA]*w[0]+w[1]);
                 _minTmp = Math.Min(_minTmp, pick_data[j].data[indexA]*w[0]+w[1]);

                 _maxB = Math.Max(_maxB, pick_data[j].data[indexB]);
                 _minB = Math.Min(_minB, pick_data[j].data[indexB]);

                _maxRangeA = Math.Max(_maxRangeA, Math.Abs(pick_data[j].data[indexA] - tmpA));
                _minRangeA = Math.Min(_minRangeA, Math.Abs(pick_data[j].data[indexA] - tmpA));

                _maxRangeB = Math.Max(_maxRangeB, Math.Abs(pick_data[j].data[indexB] - tmpB));
                _minRangeB = Math.Min(_minRangeB, Math.Abs(pick_data[j].data[indexB] - tmpB));
            }

            
            
            if(_maxB>_maxTmp)
            {
                float x = _maxTmp - _minTmp;
                float y = _maxB - _maxTmp;

                _maxA += (_maxA - _minA) * ( y/ x);
            }
            if(_minB<_minTmp)
            {
                float x = _maxTmp - _minTmp;
                float y = _minTmp - _minB;


                _minA -= (_maxA - _minA) * (y / x);
            }
            
            _maxB = Math.Max(_maxB, _maxTmp);
            _minB = Math.Min(_minB, _minTmp);
            

            //_maxB = _maxTmp;
            //_minB = _minTmp;
            if (_minA == _maxA)
            {
                //LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f*_max), Math.Abs(_min - 0.1f*_min), 2);
                LabelMinMax lmm = new LabelMinMax(_maxA + Math.Abs(0.1f * _maxA), Math.Abs(_minA - 0.1f * _minA), 2);
                labelRange[indexA] = lmm;
            }
            else
            {
                //LabelMinMax lmm = new LabelMinMax(_max, _min , Math.Min((int)((_max - _min) / ((_maxRange + _minRange) / 2))+2,10));
                LabelMinMax lmm = new LabelMinMax(_maxA + _minRangeA, _minA - _minRangeA, Math.Min((int)((_maxA - _minA) / ((_maxRangeA + _minRangeA) / 2)) + 2, 5), false);
                labelRange[indexA] = lmm;
            }

            if (_minB == _maxB)
            {
                //LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f*_max), Math.Abs(_min - 0.1f*_min), 2);
                LabelMinMax lmm = new LabelMinMax(_maxB + Math.Abs(0.1f * _maxB), Math.Abs(_minB - 0.1f * _minB), 2);
                labelRange[indexB] = lmm;
            }
            else
            {
                //LabelMinMax lmm = new LabelMinMax(_max, _min , Math.Min((int)((_max - _min) / ((_maxRange + _minRange) / 2))+2,10));
                LabelMinMax lmm = new LabelMinMax(_maxB + _minRangeB, _minB - _minRangeB, Math.Min((int)((_maxB- _minB) / ((_maxRangeB + _minRangeB) / 2)) + 2, 5), false);
                labelRange[indexB] = lmm;
            }
            //LabelMinMax lmm = new LabelMinMax(_max + Math.Abs(0.1f * _max), Math.Abs(_min - 0.1f * _min), (int)((_max - _min) / ((_maxRange + _minRange) / 2)) + 2);
            //labelRange.Add(lmm);
            
        }



        public List<float> GetDate(int n)
        {
            List<float> f = new List<float>();
            for (int i = 1; i <= n; i++)
                f.Add(i);

            return f;
        }

        public List<float> GetDateLabel(int n)
        {
            LabelMinMax lmm = new LabelMinMax(n + 1, 0, 1,false);
            return lmm.value;
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


                
                
                LabelMinMax lmm = new LabelMinMax(_max , _min, 5,false);
                labelRange.Add(lmm);
            }
        }

        public void TestFunc()
        {
              //CalculateLabelRange(SearchData("2007/1/2", "2007/1/10"));
              foreach (LabelMinMax lmm in labelRange)
                  lmm.Printf();
        }

        public List<float> ReturnRowData(int index)
        {
            List<float> f = new List<float>();

            foreach (Data d in data)
                f.Add(d.data[index]);

            return f;
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

        public LabelMinMax()
        {

        }
        public LabelMinMax(float x,float n,int r,bool flag=true)
        {
            if (flag)
            {
                max = x;
                min = n;

                max = max + 1;
                min = min - 1;
            }
            else
            {
                max = x;
                min = n;

            }
            rangeBetween = r;

            i = (max - min) / r;
            

            //Console.WriteLine(min + " " + max + " " + r + " " + i);


            value = new List<float>();

            if (flag)
            {
                for (int j = -1; j <= r + 1; j++)
                    value.Add(min + (i * j));
            }
            else
            {
                for (int j = 0; j <= r ; j++)
                    value.Add((float)Math.Round(min + (i * j)));
            }
           
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
          
            string[] s1, s2;
            s1=d1.time.Split('/');
            s2=d2.time.Split('/');

            if (Int32.Parse(s1[0]) < (Int32.Parse(s2[0])))
                return false;
            else if (Int32.Parse(s1[0]) > (Int32.Parse(s2[0])))
                return true;

            if (Int32.Parse(s1[1]) < (Int32.Parse(s2[1])))
                return false;
            else if (Int32.Parse(s1[1]) > (Int32.Parse(s2[1])))
                return true;


            if (Int32.Parse(s1[2]) < (Int32.Parse(s2[2])))
                return false;
            else if (Int32.Parse(s1[2]) > (Int32.Parse(s2[2])))
                return true;

            return false;
        }

        public static bool operator <(Data d1, Data d2)
        {
          
            return d2>d1;
        }
        public static bool operator >=(Data d1, Data d2)
        {
           
            string[] s1, s2;
            s1 = d1.time.Split('/');
            s2 = d2.time.Split('/');


            if (Int32.Parse(s1[0]) < (Int32.Parse(s2[0])))
                return false;
            else if (Int32.Parse(s1[0]) > (Int32.Parse(s2[0])))
                return true;

            if (Int32.Parse(s1[1]) < (Int32.Parse(s2[1])))
                return false;
            else if (Int32.Parse(s1[1]) > (Int32.Parse(s2[1])))
                return true;


            if (Int32.Parse(s1[2]) < (Int32.Parse(s2[2])))
                return false;
            else if (Int32.Parse(s1[2]) > (Int32.Parse(s2[2])))
                return true;

            return true;
;
            
        }
        public static bool operator <=(Data d1, Data d2)
        {



            return d2 >= d1;
        }

    }
}
