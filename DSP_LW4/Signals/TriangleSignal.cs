using System;

namespace DSP_LW4.Signals
{
    public class TriangleSignal : Signal
    {
        public TriangleSignal(double A, double F, int N) : base(A, F, N) { }

        public TriangleSignal(double A, double F, double P, int N) : base(A, F, P, N) { }

        public override double GetValue(int i)
        {
            return 2 * Amplitude / Math.PI * Math.Asin(Math.Sin((2 * Math.PI * Frequency * i / N) + Phase));
        }
    }
}
