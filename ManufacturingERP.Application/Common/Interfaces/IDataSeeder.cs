using System.Threading.Tasks;

namespace ManufacturingERP.Application.Common.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}