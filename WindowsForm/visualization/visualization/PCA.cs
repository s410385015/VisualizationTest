using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.IO;
using Accord.Math;
using Accord.Statistics.Analysis;

namespace visualization
{
    public class PCA
    {
        public PrincipalComponentAnalysis pca;
        public double a;
        public double b;
        public PCA(double[,] data)
        {


            //double[,] d = new double[5, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 }, { 9, 10 } };
            a = b = 1;
            pca = new PrincipalComponentAnalysis(data);
            pca.Compute();
            var ev = pca.ComponentMatrix;
            var m = pca.Means;
            


            a = (ev[0,1] / ev[0,0]);
            b = m[1] - m[0] * a;
        }

      
    }
}
