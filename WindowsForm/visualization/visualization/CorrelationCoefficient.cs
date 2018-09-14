using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualization
{
    class CorrelationCoefficient
    {
        private List<float> x;
        private List<float> y;
        private float x_max;
        private float x_min;
        private float y_max;
        private float y_min;
        private static float limit = 0.02f;
        public CorrelationCoefficient(List<float> _x,List<float> _y)
        {
            x = new List<float>();
            y = new List<float>();
            x = _x;
            y = _y;

            x_max = float.MinValue;
            x_min = float.MaxValue;
            y_max = float.MinValue;
            y_min = float.MaxValue;
        }
        public float CalculateCC()
        {
            float sigma_x,sigma_y,sigma_xy,sigma_x2,sigma_y2;
            int n = x.Count;
            sigma_x=sigma_y=sigma_xy=sigma_x2=sigma_y2=0;

            for(int i=0;i<n;i++)
            {
                float _x = x[i];
                float _y = y[i];

                sigma_x += _x;
                sigma_y += _y;
                sigma_xy += (_x * _y);
                sigma_x2 += (_x * _x);
                sigma_y2 += (_y * _y);

                x_max = Math.Max(x_max, _x);
                x_min = Math.Min(x_min, _x);
                y_max = Math.Max(y_max, _y);
                y_min = Math.Min(y_min, _y);
            }
            
            return ( (n * (sigma_xy) - sigma_x * sigma_y) / (float)Math.Sqrt((n * sigma_x2 - sigma_x * sigma_x) * (n * sigma_y2 - sigma_y * sigma_y)));

            
        }


        public float CalculateSimilarity()
        {
             int n=0;
             for (int i = 0; i < x.Count; i++)
             {
                 float a=(x[i]-x_min)/(x_max-x_min);
                 float b = (y[i]-y_min) / (y_max - y_min);
                 if (Math.Abs(a - b) < limit)
                     n++;

                 //Console.WriteLine(a + " " + b + " ");
             }
             
             return (float)(n /(float) x.Count);

        }
       

    }

    public class CCGroup
    {
        public int indexA;
        public int indexB;
        public float value;
        public float match;
        public CCGroup(int a,int b,float cc,float m)
        {
            indexA = a;
            indexB = b;
            value = cc;
            match = m;
        }
    }
}
