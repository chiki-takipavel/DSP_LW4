using System;

namespace DSP_LW4.Signals
{
    public class NoiseSignal : Signal
    {
        private readonly Random random = new();

        public NoiseSignal(double A, double F, int N) : base(A, F, N) { }

        public NoiseSignal(double A, double F, double P, int N) : base(A, F, P, N) { }

        public override double GetValue(int i)
        {
            return Amplitude * ((2 * random.NextDouble()) - 1);
        }
    }
}
