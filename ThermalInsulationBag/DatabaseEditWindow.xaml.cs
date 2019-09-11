using System.Windows;
using ThermalInsulationBag.Models;
using System.Data.Entity;

namespace ThermalInsulationBag
{
    /// <summary>
    /// Логика взаимодействия для DatabaseEditWindow.xaml
    /// </summary>
    public partial class DatabaseEditWindow : Window
    {
        ThermalContext db;
        public MaterialInfo selectedMaterial;
        public DatabaseEditWindow()
        {
            InitializeComponent();

            db = new ThermalContext();
            db.Materials.Load(); // загружаем данные
            materialsGrid.ItemsSource = db.Materials.Local.ToBindingList(); // устанавливаем привязку к кэшу

            this.Closing += DatabaseEditWindowClosing;
        }

        private void DatabaseEditWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (materialsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < materialsGrid.SelectedItems.Count; i++)
                {
                    selectedMaterial = materialsGrid.SelectedItems[i] as MaterialInfo;
                    if (selectedMaterial != null)
                    {
                        db.Materials.Remove(selectedMaterial);
                    }
                }
            }
            db.SaveChanges();
        }

        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            if (materialsGrid.SelectedItems.Count == 1)
            {
                selectedMaterial = materialsGrid.SelectedItems[0] as MaterialInfo;
                if (selectedMaterial != null)
                {
                    this.DialogResult = true;
                }
            }

        }
    }
}
