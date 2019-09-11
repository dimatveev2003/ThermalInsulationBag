using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ThermalInsulationBag.Models
{
    public class MaterialInfo : INotifyPropertyChanged
    {
        public MaterialInfo() { }
        public void CopyMaterial(MaterialInfo prev)
        {
            this.Id = prev.Id;
            this.Name = prev.Name;
            this.Manufacturer = prev.Manufacturer;
            this.DeliveryForm = prev.DeliveryForm;
            this.MaterialType = prev.MaterialType;
            this.ThermalConductivity = prev.ThermalConductivity;
            this.Width = prev.Width;
        }

        private int id;
        private string name;
        private string manufacturer;
        private string deliveryForm;
        private string materialType;
        private double thermalConductivity;
        private double width;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        public string DeliveryForm
        {
            get { return deliveryForm; }
            set { deliveryForm = value; OnPropertyChanged("DeliveryForm"); }
        }

        public string MaterialType
        {
            get { return materialType; }
            set { materialType = value; OnPropertyChanged("MaterialType"); }
        }
        public double ThermalConductivity
        {
            get { return thermalConductivity; }
            set { thermalConductivity = value; OnPropertyChanged("ThermalConductivity"); }
        }
        public double Width
        {
            get { return width; }
            set { width = value; OnPropertyChanged("Width"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
