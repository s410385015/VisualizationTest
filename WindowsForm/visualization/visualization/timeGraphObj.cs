using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualization
{
    public class timeGraphObj
    {
        public List<float> x;
        public List<float> y;
        public float a;
        public float b;

        public timeGraphObj()
        {
            x = new List<float>();
            y = new List<float>();
            a = 1;
            b = 0;
        }

       
        public void SetData(List<float> _x,List<float> _y)
        {
            x = _x;
            y = _y;
        }

        public void CalculateRegression()
        {
            List<float> _x = new List<float>();
            for (int i = 0; i < x.Count; i++)
            {
                _x.Add(x[i]);
                _x.Add(1);
            }
            matrix ma = new matrix(x.Count, 2, _x);
            matrix mb = new matrix(y.Count, 1, y);
            matrix mata = matrix.Mul(ma.Transpose(), ma);
            mata = mata.Invert();

            matrix coff = matrix.Mul(matrix.Mul(mata, ma.Transpose()), mb);
            a = coff.m[0];
            b = coff.m[1];
        }
        
        public List<float> GetDiff(List<float> _x,List<float> _y)
        {
            List<float> diff = new List<float>();

            for (int i = 0; i < _x.Count;i++ )
            {
                diff.Add(_x[i] * a + b - _y[i]);
            }

            return diff;
        }

        public static float GetMax(List<float> d)
        {
            float max = 0;

            foreach (float f in d)
                max = Math.Max(Math.Abs(f), max);

            return max;
        }
    }
}
