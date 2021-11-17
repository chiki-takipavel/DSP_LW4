using DSP_LW4.Correlations;
using DSP_LW4.Models;
using DSP_LW4.Signals;
using DSP_LW4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DSP_LW4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ChartViewModel chart;

        private delegate Signal GetSignal(ParametersModel model, int index = 0);

        private readonly List<GetSignal> getSignals = new()
        {
            SignalCreator.GetSinusoid,
            SignalCreator.GetPulse,
            SignalCreator.GetTriangle,
            SignalCreator.GetSawTooth,
            SignalCreator.GetNoise
        };

        public MainWindow()
        {
            InitializeComponent();
            chart = (ChartViewModel)DataContext;
        }

        private async void GenerateClick(object sender, RoutedEventArgs e)
        {
            ParametersModel model;
            Correlation corr;
            if (rbAutoCorrelation.IsChecked is true)
            {
                model = ParametersGetter.GetSignal(this);
                model.N = Convert.ToInt32(tbN.Text);
                Signal signal = getSignals[cmbSignalType.SelectedIndex](model, 0);
                corr = new(signal);
            }
            else
            {
                model = ParametersGetter.GetTwoSignals(this);
                model.N = Convert.ToInt32(tbN.Text);
                Signal signalA = getSignals[cmbSignalType.SelectedIndex](model, 0);
                Signal signalB = getSignals[cmbSignalType1.SelectedIndex](model, 1);
                corr = new(signalA, signalB);
            }

            long startTime = Environment.TickCount64;
            IEnumerable<double> simpleResult = await Task.Run(() => corr.GenerateSimple());
            long endTime = Environment.TickCount64;
            long timeSimpleResult = endTime - startTime;
            tblSimpleTime.Text = $"Прямое вычисление: {timeSimpleResult} мс";

            startTime = Environment.TickCount;
            IEnumerable<double> fastResult = await Task.Run(() => corr.GenerateFast());
            endTime = Environment.TickCount;
            long timeFastResult = endTime - startTime;
            tblFastTime.Text = $"Быстрое вычисление: {timeFastResult} мс";

            await Task.Run(() => chart.CreateChart(simpleResult.Select(x => (float)x), fastResult.Select(x => (float)x)));
        }
    }
}
