using DSP_LW4.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DSP_LW4.Correlations
{
    public class Correlation
    {
        private readonly double[] signalA;
        private readonly double[] signalB;
        private Complex[] complexA;
        private Complex[] complexB;
        private readonly double normalRate;

        public Correlation(Signal a)
        {
            a.Generate();
            this.signalB = this.signalA = a.Values;
            this.complexB = this.complexA = this.signalA.Select(x => (Complex)x).ToArray();
            Array.Resize(ref complexA, complexA.Length * 2);
            Array.Resize(ref complexB, complexB.Length * 2);
            this.normalRate = GetNormalRate();
        }

        public Correlation(Signal a, Signal b)
        {
            a.Generate();
            b.Generate();
            this.signalA = a.Values;
            this.signalB = b.Values;
            this.complexA = this.signalA.Select(x => (Complex)x).ToArray();
            this.complexB = this.signalB.Select(x => (Complex)x).ToArray();
            Array.Resize(ref complexA, complexA.Length * 2);
            Array.Resize(ref complexB, complexB.Length * 2);
            this.normalRate = GetNormalRate();
        }

        public IEnumerable<double> GenerateSimple()
        {
            IEnumerable<double> left = GetSimple(true);
            IEnumerable<double> right = GetSimple(false);

            return left.Concat(right);
        }

        public IEnumerable<double> GenerateFast()
        {
            IEnumerable<double> left = GetFast(true);
            Swap(ref complexA, ref complexB);
            IEnumerable<double> right = GetFast(false);
            Swap(ref complexA, ref complexB);

            return left.Concat(right);
        }

        public IEnumerable<double> GetSimple(bool leftToCenter)
        {
            List<double> result = new();
            if (leftToCenter)
            {
                for (int bias = signalB.Length - 1; bias >= 0; bias--)
                {
                    double corr = 0;
                    for (int i = 0; i < signalA.Length - bias; i++)
                    {
                        corr += signalA[i] * signalB[i + bias];
                    }

                    corr /= signalA.Length;
                    corr /= normalRate;
                    result.Add(corr);
                }
            }
            else
            {
                for (int bias = 1; bias < signalA.Length; bias++)
                {
                    double corr = 0;
                    for (int i = 0; i < signalB.Length - bias; i++)
                    {
                        corr += signalA[i + bias] * signalB[i];
                    }

                    corr /= signalA.Length;
                    corr /= normalRate;
                    result.Add(corr);
                }
            }

            return result;
        }

        public IEnumerable<double> GetFast(bool leftToCenter)
        {
            List<double> result = new();
            Complex[] complexX1 = Fft.GetFftButterfly(complexA);
            Complex[] complexX2 = Fft.GetFftButterfly(complexB);
            Complex[] complexResult = new Complex[complexX1.Length];

            for (int i = 0; i < complexX1.Length; i++)
            {
                complexX1[i] = Complex.Conjugate(complexX1[i]);
                complexResult[i] = complexX1[i] * complexX2[i];
            }

            complexResult = Fft.GetFftButterfly(complexResult, true);

            int resultLength = complexResult.Length / 2;
            if (leftToCenter)
            {
                for (int i = resultLength - 1; i >= 0; i--)
                {
                    double realResult = complexResult[i].Real;
                    realResult /= signalA.Length;
                    realResult /= normalRate;
                    result.Add(realResult);
                }
            }
            else
            {
                for (int i = 1; i < resultLength; i++)
                {
                    double realResult = complexResult[i].Real;
                    realResult /= signalA.Length;
                    realResult /= normalRate;
                    result.Add(realResult);
                }
            }

            return result;
        }

        private double GetNormalRate()
        {
            float sumA = 0, sumB = 0;
            for (int i = 0; i < signalA.Length; i++)
            {
                sumA += (float)Math.Pow(signalA[i], 2);
                sumB += (float)Math.Pow(signalB[i], 2);
            }

            return 1d / signalA.Length * (float)Math.Sqrt(sumA * sumB);
        }

        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
