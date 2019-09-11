using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Windows;

namespace ThermalInsulationBag
{
    /// <summary>
    /// Логика взаимодействия для GraphicWindow.xaml
    /// </summary>
    public partial class GraphicWindow : Window
    {
        private PlotModel model = new PlotModel();
        private PlotModel modelLoss = new PlotModel();
        public GraphicWindow() { }
        public GraphicWindow(List<DataPoint> tempPoints, List<DataPoint> lossPoints)
        {
            InitializeComponent();
            LineSeries line = new LineSeries();
            foreach (var point in tempPoints)
            {
                line.Points.Add(point);
            }
            model.Series.Add(line);
            model.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Title = "ч", MajorGridlineColor = OxyColors.Black, MajorGridlineStyle = LineStyle.Automatic, TitlePosition = 1});
            model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Title = "T, °С", MajorGridlineColor = OxyColors.Black, MajorGridlineStyle = LineStyle.Automatic, TitlePosition = 0.95 });
            plotTemp.Model = model;
            plotTemp.InvalidatePlot(true);
            line = new LineSeries();
            foreach (var point in lossPoints)
            {
                line.Points.Add(point);
            }
            modelLoss.Series.Add(line);
            modelLoss.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Title = "ч", MajorGridlineColor = OxyColors.Black, MajorGridlineStyle = LineStyle.Automatic, TitlePosition = 1 });
            modelLoss.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Title = "Q, Вт", MajorGridlineColor = OxyColors.Black, MajorGridlineStyle = LineStyle.Automatic, TitlePosition = 0.95 });
            plotLoss.Model = modelLoss;
            plotLoss.InvalidatePlot(true);
        }

        private void SaveGraphTemp(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Имя файла";
            saveFileDialog.DefaultExt = "bmp";
            if (saveFileDialog.ShowDialog() == true)
                plotTemp.SaveBitmap(saveFileDialog.FileName);
        }

        private void SaveGraphLoss(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Имя файла";
            saveFileDialog.DefaultExt = "bmp";
            if (saveFileDialog.ShowDialog() == true)
                plotLoss.SaveBitmap(saveFileDialog.FileName);
        }
    }
}
