using System;

namespace DSP_LW4.Signals
{
    public abstract class Signal
    {
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double Phase { get; set; }
        public int N { get; set; }
        public double[] Values { get; set; }

        protected double DefaultPhase { get; } = Math.PI / 6;

        protected Signal(double A, double F, double P, int N)
        {
            Amplitude = A;
            Frequency = F;
            Phase = P;
            this.N = N;
        }

        protected Signal(double A, double F, int N)
        {
            Amplitude = A;
            Frequency = F;
            Phase = 0;
            this.N = N;
        }

        public void Generate()
        {
            Values = GetValues();
        }

        protected double[] GetValues()
        {
            double[] values = new double[N];
            for (int i = 0; i < N; i++)
            {
                values[i] = GetValue(i);
            }

            return values;
        }

        public abstract double GetValue(int i);
    }
}
