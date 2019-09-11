namespace ThermalInsulationBag.Models
{
    public class InputParams
    {
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float TempOut { get; set; }
        public float TempIn { get; set; }
        public float Coef2 { get; set; }
        public float Coef1 { get; set; }
        public float TimeStep { get; set; }
        public float BagFilling { get; set; }
        public float HeatCapacityInBag { get; set; }
        public float BagDensity { get; set; }
        public float FinalTemp { get; set; }
        public float PowerHeatElement { get; set; }
    }
}
