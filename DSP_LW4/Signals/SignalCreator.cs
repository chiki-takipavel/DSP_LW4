using DSP_LW4.Models;

namespace DSP_LW4.Signals
{
    public static class SignalCreator
    {
        public static Signal GetSinusoid(ParametersModel model, int Index = 0)
        {
            return new SinusoidSignal(model.A[Index], model.F[Index], model.P[Index], model.N);
        }

        public static Signal GetPulse(ParametersModel model, int Index = 0)
        {
            return new PulseWithDifferentDutyCycleSignal(model.A[Index], model.F[Index], model.P[Index], model.N, model.WellRate[Index]);
        }

        public static Signal GetTriangle(ParametersModel model, int Index = 0)
        {
            return new TriangleSignal(model.A[Index], model.F[Index], model.P[Index], model.N);
        }

        public static Signal GetSawTooth(ParametersModel model, int Index = 0)
        {
            return new SawToothSignal(model.A[Index], model.F[Index], model.P[Index], model.N);
        }

        public static Signal GetNoise(ParametersModel model, int Index = 0)
        {
            return new NoiseSignal(model.A[Index], model.F[Index], model.P[Index], model.N);
        }
    }
}
