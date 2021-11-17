namespace DSP_LW4.Models
{
    public class ParametersModel
    {
        public double[] A { get; set; } = new double[2];
        public double[] F { get; set; } = new double[2];
        public double[] P { get; set; } = new double[2];
        public double[] WellRate { get; set; } = new double[2];
        public int N { get; set; }
    }
}
