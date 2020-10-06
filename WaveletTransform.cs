using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveletProcSolution
{
    public class WaveletTransform
    {
        public readonly float[] arrayAn;
        public WaveletTransform(float[] array)
        {
            this.arrayAn = array;
        }              
        public float[] FullWaveletTransform()
        {           
            return FullWaveletTransformRec(arrayAn);
        }
        private float[] FullWaveletTransformRec(float[] subArr)
        {            
            if (subArr.Length == 1) 
                return subArr;
            float[] temp = PartitialWaveletTransform(subArr);
            float[] left = new float[subArr.Length / 2];
            float[] right = new float[subArr.Length / 2];
            

            Array.Copy(temp, 0, left, 0, left.Length);
            Array.Copy(temp, temp.Length / 2, right, 0, right.Length);

            left = FullWaveletTransformRec(left);

            float[] leftright = new float[left.Length + right.Length];
            left.CopyTo(leftright, 0);
            right.CopyTo(leftright, left.Length);
            return leftright;
        }
        public float[] PartitialWaveletTransform(float[] subArray = null)
        {
            float[] result = new float[arrayAn.Length];
            FindAverageOrDetailing(subArray ?? arrayAn, (a, b) => (a + b) / 2)
                .CopyTo(result, 0);
            FindAverageOrDetailing(subArray ?? arrayAn, (a, b) => (a - b) / 2)
                .CopyTo(result, arrayAn.Length / 2);
            return result;
        }
        
        private float[] FindAverageOrDetailing(float[] subArray, Func<float, float, float> func)
        {
            float[] result = new float[subArray.Length / 2];
            int j = 0;
            for (int i = 0; i < subArray.Length; i += 2)
            {
                result[j] = func(subArray[i], subArray[i + 1]);
                j++;
            }
            return result;
        }

        public float[] FullWaveletTransformReverse()
        {
            return FullWeveletTransformReverseRec(arrayAn);
        }
        private float[] FullWeveletTransformReverseRec(float[] subArr)
        {
            float[] left = new float[subArr.Length / 2];
            float[] right = new float[subArr.Length / 2];
            Array.Copy(subArr, 0, left, 0, left.Length);
            Array.Copy(subArr, subArr.Length / 2, right, 0, right.Length);

            if (subArr.Length > 2)              
                left = FullWeveletTransformReverseRec(left);

            float[] leftright = ReestablishFromAverageAndDetailing(left, right);
            return leftright;
        }
        private float[] ReestablishFromAverageAndDetailing(float[] a, float[] b)
        {
            float[] result = new float[a.Length + b.Length];
            for(var i = 1; i <= result.Length; i++)
            {
                int k = ((i + 1) / 2);
                int q = (int)Math.Pow(-1, i+1);
                result[i-1] = a[k-1]+(q*b[k-1]);
            }
            return result;    
        }
        
        public double PixelAverageError(float[] arrayBn)
        {
            int mn = arrayBn.Length;
            double sum = 0;
            for(int i = 0; i < arrayBn.Length; i++)
            {
                sum += Math.Abs(arrayAn[i] - arrayBn[i]);
            }
            return sum / mn;
        }
        public double SquaredAvarageError(float[] arrayBn)
        {
            int mn = arrayBn.Length;
            double sum = 0;
            for (int i = 0; i < arrayBn.Length; i++)
            {
                sum += Math.Abs(arrayAn[i] - arrayBn[i]) * Math.Abs(arrayAn[i] - arrayBn[i]);
            }
            return Math.Sqrt(sum / mn);
        }

        public double InfinityMetric(float[] arrayBn)
        {
            float[] diff = new float[arrayBn.Length];
            for (int i = 0; i < diff.Length; i++)
            {
                diff[i] = Math.Abs(arrayAn[i] - arrayBn[i]);
            }
            return diff.Max();
        }

        public double PeakSignalTONoiseRatio(float[] arrayBn, int deep)
        {
            double M = Math.Pow(2, deep) - 1;
            int mn = arrayBn.Length;
            double sum = 0;
            for (int i = 0; i < arrayBn.Length; i++)
            {
                sum += Math.Abs(arrayAn[i] - arrayBn[i]) * Math.Abs(arrayAn[i] - arrayBn[i]);
            }
            double temp = Math.Pow(M, 2) * mn / sum;
            return 10 * Math.Log10(temp);
        }
    }
}
