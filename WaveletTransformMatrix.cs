using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace WaveletProcSolution
{
    class WaveletTransformMatrix
    {
        float[,] matrixAn;

        public WaveletTransformMatrix(float[,] matrixAn)
        {
            this.matrixAn = matrixAn;
        }

        int ColRowCount { get { return (int)Math.Sqrt(matrixAn.Length); } }
        public float[] GetRow(int i)
        {
            float[] temp = new float[ColRowCount];
            for (int j=0; j< ColRowCount; j++)
            {
                temp[j] = matrixAn[i, j];
            }
            return temp;
        }

        public float[] GetCol(int j)
        {
            float[] temp = new float[ColRowCount];
            for (int i = 0; i < ColRowCount; i++)
            {
                temp[i] = matrixAn[i, j];
            }
            return temp;
        }

        public void SetRow(int index, float[] to)
        {
            for (int j = 0; j < ColRowCount; j++)
            {
                matrixAn[index, j] = to[j];
            }
        }

        public void SetCol(int index, float[] to)
        {
            for (int i = 0; i < ColRowCount; i++)
            {
                matrixAn[i, index] = to[i];
            }
        }
        public float[,] StandartMatrixWavelet()
        {
            float[] temp;
            for (int i=0; i< ColRowCount; i++)
            {
                temp = GetRow(i);
                WaveletTransform waveletTransform = new WaveletTransform(temp);
                SetRow(i, waveletTransform.FullWaveletTransform());
            }
            for (int j = 0; j < ColRowCount; j++)
            {
                temp = GetCol(j);
                WaveletTransform waveletTransform = new WaveletTransform(temp);
                SetCol(j, waveletTransform.FullWaveletTransform());
            }
            return matrixAn;
        }


        
        
    }
}
