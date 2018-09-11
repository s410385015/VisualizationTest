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
        

        public CorrelationCoefficient(List<float> _x,List<float> _y)
        {
            x = new List<float>();
            y = new List<float>();
            x = _x;
            y = _y;
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
   
            }

            return ( (n * (sigma_xy) - sigma_x * sigma_y) / (float)Math.Sqrt((n * sigma_x2 - sigma_x * sigma_x) * (n * sigma_y2 - sigma_y * sigma_y)));

            
        }
    }

    public class CCGroup
    {
        public int indexA;
        public int indexB;
        public float value;

        public CCGroup(int a,int b,float cc)
        {
            indexA = a;
            indexB = b;
            value = cc;
        }
    }
}
