using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveletProcSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            WaveletTransform waveletTransform = new WaveletTransform(new float[] { 1, 2, 3, 4, 5,6,7,8 });
            var r = waveletTransform.FullWaveletTransform();
            foreach(var i in r)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            var t = waveletTransform.FullWeveletTransformReverse(r);
            foreach (var i in t)
            {
                Console.Write($"{i} ");
            }
            Console.ReadKey();
        }
    }
}
