using System;

namespace DSP_LW4.Signals
{
    public class SawToothSignal : Signal
    {
        public SawToothSignal(double A, double F, int N) : base(A, F, N) { }

        public SawToothSignal(double A, double F, double P, int N) : base(A, F, P, N) { }

        public override double GetValue(int i)
        {
            return Amplitude * ((-1 / Math.PI * Math.Atan(1 / Math.Tan((Math.PI * Frequency * i / N) + Phase))) + 0.5);
        }
    }
}
