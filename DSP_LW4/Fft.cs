using System;
using System.Numerics;

namespace DSP_LW4
{
    public static class Fft
    {
        private static Complex W(int k, int N, bool invert)
        {
            if (k % N == 0)
            {
                return 1;
            }

            double arg = -2 * Math.PI * k / N * (invert ? -1 : 1);
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }

        public static Complex[] GetFft(Complex[] x, bool invert = false)
        {
            Complex[] result;
            int n = x.Length;
            if (n == 2)
            {
                result = new Complex[2];
                result[0] = x[0] + x[1];
                result[1] = x[0] - x[1];

                if (invert)
                {
                    result[0] /= 2;
                    result[1] /= 2;
                }
            }
            else
            {
                Complex[] xEvenTemp = new Complex[n / 2];
                Complex[] xOddTemp = new Complex[n / 2];
                for (int i = 0; i < n / 2; i++)
                {
                    xEvenTemp[i] = x[2 * i];
                    xOddTemp[i] = x[(2 * i) + 1];
                }

                Complex[] xEven = GetFft(xEvenTemp, invert);
                Complex[] xOdd = GetFft(xOddTemp, invert);
                result = new Complex[n];
                for (int i = 0; i < n / 2; i++)
                {
                    result[i] = xEven[i] + (W(i, n, invert) * xOdd[i]);
                    result[i + (n / 2)] = xEven[i] - (W(i, n, invert) * xOdd[i]);
                    if (invert)
                    {
                        result[i] /= 2;
                        result[i + (n / 2)] /= 2;
                    }
                }
            }

            return result;
        }

        public static Complex[] GetFftIterative(Complex[] x, bool invert = false)
        {
            int n = x.Length;
            Complex[] result = new Complex[n];
            int log2N = (int)Math.Log2(n);
            for (uint i = 0; i < n; i++)
            {
                uint rev = BitReverse(i, log2N);
                result[i] = x[rev];
            }

            Complex J = new(0, 1);
            for (int s = 1; s <= log2N; s++)
            {
                int m = 1 << s; // 2 power s
                int m2 = m >> 1; // m2 = m/2 -1
                Complex w = new(1, 0);

                Complex wm = Complex.Exp(J * (Math.PI / m2) * (invert ? -1 : 1));
                for (int j = 0; j < m2; ++j)
                {
                    for (int k = j; k < n; k += m)
                    {
                        // t = twiddle factor
                        Complex t = w * result[k + m2];
                        Complex u = result[k];
                        // Similar calculating y[k]
                        result[k] = u + t;
                        // Similar calculating y[k+n/2]
                        result[k + m2] = u - t;

                        if (invert)
                        {
                            result[k] /= 2;
                            result[k + m2] /= 2;
                        }
                    }

                    w *= wm;
                }
            }

            return result;
        }

        private static uint BitReverse(uint x, int log2N)
        {
            uint n = 0;
            for (int i = 0; i < log2N; i++)
            {
                n <<= 1;
                n |= x & 1;
                x >>= 1;
            }

            return n;
        }
    }
}
