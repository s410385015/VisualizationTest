using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace visualization
{
    public class matrix
    {
        public List<float> m;
        public int w;
        public int h;

        public matrix()
        {
            m = new List<float>();
        }

        public matrix(int _h, int _w)
        {
            m = new List<float>();
            w = _w;
            h = _h;
            for (int i = 0; i < w * h; i++)
                m.Add(0);
        }
        public matrix(int _h,int _w,List<float> f)
        {
            m = new List<float>();
            m = f;
            w = _w;
            h = _h;
        }

        public matrix identityMatrix(int s)
        {
            matrix iMatrix = new matrix(s, s);
            for (int i = 0; i < s; i++)
                iMatrix.m[(s + 1) * i] = 1.0f;
            return iMatrix;
        }

        public static matrix Mul(matrix a,matrix b)
        {
            matrix r=new matrix(a.h, b.w);
	        float value;
	        for (int i = 0; i < a.h; i++){
		        for (int j = 0; j < b.w; j++){
			        value = 0;
			        for (int k = 0; k < a.w; k++){
				
				        value += a.m[i*a.w+k] * b.m[k*b.w+j];
				       
			        }
			
			        r.m[i*r.w+j] = value;
		        }
	        }

	        return r;
        }

        public matrix Invert()
        {
            matrix L = identityMatrix(w);
	        matrix U=new matrix(h,w,m);
	
	        for (int i = 0; i < h; i++)
	        {
		        matrix tmp = identityMatrix(w);
		        matrix tmp1 = identityMatrix(w);
		        for (int j = i + 1; j < h; j++)
		        {
			        tmp.m[j*w+i] = -U.m[j*w+i] / U.m[i*w+i];
			        tmp1.m[j*w + i] = U.m[j*w + i] / U.m[i*w+i];
		        }
		
		        L = Mul(L, tmp1);
		        U = Mul(tmp, U);
		
	        }
	
	        matrix Inv=new matrix(L.h, L.h);
	        for (int k = 0; k < L.h; k++)
	        {
		        matrix _base=new matrix(1,L.h);
		        _base.m[k] = 1.0f;
		        matrix X=new matrix(1,L.h);
		        matrix Y=new matrix(1, L.h);
		        for (int i = 0; i < L.h;i++)
		        {
			        for (int j = 0; j < i; j++)
			        {
				        Y.m[i] -= L.m[i*L.w+j] * Y.m[j];
			        }
			        Y.m[i] += L.m[i*L.w+i] * _base.m[i];
		        }
		        for (int i = L.h - 1; i>-1; i--)
		        {
			        for (int j = i + 1; j < L.h; j++)
			        {
				        X.m[i] -= U.m [i*U.w+j] * X.m[j];
			        }
			        X.m[i] += Y.m[i];
			        X.m[i] /= U.m[i*(U.w+1)];
		        }
		        for (int i = 0; i < X.w; i++)
			        Inv.m[i*Inv.w+k] = X.m[i];

		
		
	        }




	
	
	        return Inv;
        }

        public matrix Transpose()
        {
            matrix mt = new matrix(w, h);

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    mt.m[j * h + i] = m[i * w  +j];

            
            return mt;
        }
    }
}
