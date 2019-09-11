using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ThermalInsulationBag.Models;

namespace ThermalInsulationBag
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ReturnMaterialInfo()
        {
            return $"Информация о используемом материале\nНазвание материала - { material.Name}\nПроизводитель - {material.Manufacturer}\nФорма поставки - {material.DeliveryForm}\nТип материала - {material.MaterialType}\nКоэффициент теплопроводности - {material.ThermalConductivity} Вт/(м·К)\nТолщина - {material.Width} мм\n\n";
        }

        public string ReturnInputParamsInfo()
        {
            return $"Входные данные\nГабаритные параметры сумки, м: длина - {inputParams.Length}, ширина - {inputParams.Width}, высота - {inputParams.Height};\nТемпература окружающей среды - {inputParams.TempOut} °С\nТемпература внутри сумки - {inputParams.TempIn} °С\n" +
                $"Коэффициент теплоотдачи с внешней поверхности сумки - {inputParams.Coef2} Вт/м^2·К\nКоэффициент теплоотдачи с внутренней поверхности сумки - {inputParams.Coef2} Вт/м^2·К\n" +
                $"Шаг по времени - {inputParams.TimeStep} с\nСтепень заполнения сумки - {inputParams.BagFilling} м^3/м^3\nТеплоемкость содержимого сумки - {inputParams.HeatCapacityInBag} Дж/(кг·К)\nПлотность содержимого сумки - {inputParams.BagDensity} кг/м^3\nКонечная температура содержимого сумки - {inputParams.FinalTemp} °С\nПолезная мощность нагревательного элемента - {inputParams.PowerHeatElement} Вт\n\n";
        }

        InputParams inputParams;
        MaterialInfo material;
        public bool LossMode { get; set; }
        public bool TimeMode { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            inputParams = new InputParams { Coef1 = 5, Coef2 = 5, TimeStep = 10, HeatCapacityInBag = 4200, BagDensity = 1000 };
            material = new MaterialInfo();
            this.InputParameters.DataContext = inputParams;
            ModeChecks.DataContext = this;
            this.materialInf.DataContext = material;
        }

        private void DbEditClick(object sender, RoutedEventArgs e)
        {
            DatabaseEditWindow databaseEditWindow = new DatabaseEditWindow();
            if (databaseEditWindow.ShowDialog() == true)
            {
                material.CopyMaterial(databaseEditWindow.selectedMaterial);
            }
        }

        private void BoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789.-".IndexOf(e.Text) < 0;
        }

        private void StartSimulateClick(object sender, RoutedEventArgs e)
        {
            OutputBox.Text += ReturnMaterialInfo() + ReturnInputParamsInfo();
            if (LossMode && TimeMode)
            {
                OutputBox.Text += $"Время охлаждения {Solver.TimeCooling(inputParams, material) / 60} мин = {Solver.TimeCooling(inputParams, material) / 3600} ч\n";
                OutputBox.Text += $"Тепловые потери {Solver.ThermalLoss(inputParams, material)} Вт\n";

            }
            else if (LossMode)
            {
                OutputBox.Text += $"Тепловые потери {Solver.ThermalLoss(inputParams, material)} Вт\n";
            }
            OutputBox.Text += "\n--------------------------------------\n\n";
            OutputBox.ScrollToEnd();
            Graphic.IsEnabled = true;
        }

        private void WriteFileClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "Имя файла";
            saveFile.DefaultExt = "log";
            if (saveFile.ShowDialog() == true)
                File.AppendAllText(saveFile.FileName, OutputBox.Text);
        }

        private void TimeModeCheckChecked(object sender, RoutedEventArgs e)
        {
            LossModeCheck.IsChecked = true;
        }

        private void GraphicClick(object sender, RoutedEventArgs e)
        {
            GraphicWindow graphic = new GraphicWindow(Solver.TempPts, Solver.LossPts);
            graphic.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
