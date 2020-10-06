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
        private readonly float[] array;
        public int Count { get { return array.Length; } }
        public WaveletTransform(float[] array)
        {
            this.array = array;
        }
       
        public float[] PartitialWaveletTransform(float[] subArray)
        {
            float[] result = new float[array.Length];
            FindAverageOrDetailing(subArray,(a, b) => (a + b) / 2).CopyTo(result, 0);
            FindAverageOrDetailing(subArray, (a, b) => (a - b) / 2).CopyTo(result, array.Length/2);
            return result;
        }
        public float[] FullWaveletTransform()
        {           
            return FullWaveletTransformRec(array);
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

        public float[] FullWeveletTransformReverse(float[] subArr)
        {
            float[] left = new float[subArr.Length / 2];
            float[] right = new float[subArr.Length / 2];
            Array.Copy(subArr, 0, left, 0, left.Length);
            Array.Copy(subArr, subArr.Length / 2, right, 0, right.Length);

            if (subArr.Length > 2)
            {                
                left = FullWeveletTransformReverse(left);
            }

            float[] leftright = ff(left, right);
            return leftright;
        }
        private float[] ff(float[] a, float[] b)
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
        public float[] FindAverageOrDetailing(float[] subArray, Func<float, float, float> func)
        {
            float[] result = new float[subArray.Length/2];
            int j = 0;
            for (int i = 0; i < subArray.Length; i+=2)
            {
                result[j] = func(subArray[i], subArray[i + 1]);
                j++;
            }
            return result;
        }
        
        private bool step(int a)
        {
            if (a == 2) return true;
            else if (a % 2 == 0) return step(a / 2);
            else return false;
        }
        
    }
}
