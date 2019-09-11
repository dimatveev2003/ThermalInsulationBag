using System.Data.Entity;

namespace ThermalInsulationBag.Models
{
    class ThermalContext : DbContext
    {
        public ThermalContext() : base("DbConnection")
        {
            
        }
        public DbSet<MaterialInfo> Materials { get; set; }
    }
}
