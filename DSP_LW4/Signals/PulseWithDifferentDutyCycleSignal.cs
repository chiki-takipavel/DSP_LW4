using System;

namespace DSP_LW4.Signals
{
    public class PulseWithDifferentDutyCycleSignal : Signal
    {
        public double WellRate { get; set; }

        public PulseWithDifferentDutyCycleSignal(double A, double F, int N, double wellRate) : base(A, F, N)
        {
            WellRate = wellRate;
        }

        public PulseWithDifferentDutyCycleSignal(double A, double F, double P, int N, double wellRate) : base(A, F, P, N)
        {
            WellRate = wellRate;
        }

        public override double GetValue(int i)
        {
            return Amplitude * GetImpulse(i);
        }

        private double GetImpulse(int n)
        {
            double sin = Math.Sin((2 * Math.PI * Frequency * n / N) + Phase) + 1;
            return sin >= (1 - WellRate) * 2 ? 1 : 0;
        }
    }
}
