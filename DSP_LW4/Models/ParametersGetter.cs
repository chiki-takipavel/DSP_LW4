using System;

namespace DSP_LW4.Models
{
    public static class ParametersGetter
    {
        public static ParametersModel GetSignal(MainWindow window)
        {
            ParametersModel result = new();
            result.A[0] = Convert.ToDouble(window.tbAmplitude.Text);
            result.F[0] = Convert.ToDouble(window.tbFrequency.Text);
            result.WellRate[0] = window.slDutyCycle.Value;

            return result;
        }

        public static ParametersModel GetTwoSignals(MainWindow window)
        {
            ParametersModel result = new();
            result.A[0] = Convert.ToDouble(window.tbAmplitude.Text);
            result.F[0] = Convert.ToDouble(window.tbFrequency.Text);
            result.A[1] = Convert.ToDouble(window.tbAmplitude1.Text);
            result.F[1] = Convert.ToDouble(window.tbFrequency1.Text);
            result.WellRate[0] = Convert.ToDouble(window.slDutyCycle.Value);
            result.WellRate[1] = Convert.ToDouble(window.slDutyCycle1.Value);

            return result;
        }
    }
}
