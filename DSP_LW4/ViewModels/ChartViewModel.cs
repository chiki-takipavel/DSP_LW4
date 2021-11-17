using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DSP_LW4.ViewModels
{
    public class ChartViewModel
    {
        public ObservableCollection<ISeries> Series { get; set; }
        public ObservableCollection<Axis> XAxes { get; set; }
        public ObservableCollection<Axis> YAxes { get; set; }

        public ChartViewModel()
        {
            Series = new ObservableCollection<ISeries>();

            XAxes = new ObservableCollection<Axis> { new Axis() };
            YAxes = new ObservableCollection<Axis> { new Axis() };
        }

        public void CreateChart(params IEnumerable<float>[] values)
        {
            Series.Clear();
            XAxes.Clear();
            YAxes.Clear();

            XAxes.Add(new Axis
            {
                Labeler = value => $"{value}"
            });

            YAxes.Add(new Axis
            {
                MinLimit = -1.2,
                MaxLimit = 1.2
            });

            foreach (var value in values)
            {
                var lineSeries = new LineSeries<float>
                {
                    Name = string.Empty,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    Values = value
                };

                Series.Add(lineSeries);
            }
        }
    }
}
