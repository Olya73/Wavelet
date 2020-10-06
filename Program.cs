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
            WaveletTransform waveletTransform = new WaveletTransform(new float[] { 249,247,243,241,180,184,235,237 });
            var r = waveletTransform.FullWaveletTransform();
            foreach(var i in r)
            {
                Console.Write($"{i}, ");
            }
            Console.WriteLine();
            WaveletTransform waveletTransform1 = new WaveletTransform(new float[] { 227, 18, 3, -27, 0, 0, -2, 0 });
            var t = waveletTransform1.FullWaveletTransformReverse();
            foreach (var i in t)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            var d1 = waveletTransform.PixelAverageError(t);
            var d2 = waveletTransform.SquaredAvarageError(t);
            var dI = waveletTransform.InfinityMetric(t);
            var dPSNR = waveletTransform.PeakSignalTONoiseRatio(t, 8);
            Console.WriteLine($"d1: {d1} d2: {d2} dI: {dI} dPSNR: {dPSNR}");
            Console.ReadKey();
        }
    }
}
