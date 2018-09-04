using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace visualization
{
    class ExecutePython
    {
        public List<string> label;
        public List<string> date;
        public List<List<float>> data;

        public ExecutePython()
        {

        }

        public void Ececute()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "dist/DataProcessing.exe";
            //start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string label_str = reader.ReadLine();
                    
                    label = new List<string>();
                    string[] raw_label = label_str.Split(',');
                    for (int i = 0; i < raw_label.Length; i++)
                        label.Add(raw_label[i]);

                    //Console.WriteLine(label_str);


                    string date_info = reader.ReadLine();
                    date = new List<string>();
                    string[] raw_date = date_info.Split(',');
                    for (int i = 0; i < raw_date.Length; i++)
                        date.Add(raw_date[i]);

                    string data_str=reader.ReadToEnd();
                    data = new List<List<float>>();
                    string[] raw_data = data_str.Split('\n');
                    //Console.WriteLine(raw_data[0]);
                    for(int i=0;i<raw_data.Length;i++)
                    {
                        string[] raw = raw_data[i].Split(',');
                        //Console.WriteLine(raw.Length);
                        List<float> f = new List<float>();
                        for (int j = 0; j < raw.Length; j++)
                        {
                            try
                            {
                                f.Add(float.Parse(raw[j]));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(i + "-" + j + "-" + e.ToString());
                            }
                        }
                        //Console.WriteLine(f.Count);
                        data.Add(f);
                    }



                }
            }

           
            
        }

        public void TestFunc()
        {
            //foreach (string t in date)
                //Console.WriteLine(t);
        }
    }
}
