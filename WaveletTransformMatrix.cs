using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveletProcSolution
{
    class WaveletTransformMatrix
    {
        WaveletTransform[] matrix;

        public WaveletTransformMatrix(float[,] arr)
        {
            int col = (int)Math.Sqrt(arr.Length);
            float[] array = new float[col];
            matrix = new WaveletTransform[col];
            int j = 0;
            int row = 0;
            for(var i=0; i< arr.Length; i++)
            {
                if (j < col)
                {
                    array[j] = arr[row, j];
                    j++;
                } 
                else
                {
                    matrix[row] = new WaveletTransform(array);
                    array = new float[col];
                    j = 0;
                    i--;
                    row++;
                }
            }
        }
        
    }
}
