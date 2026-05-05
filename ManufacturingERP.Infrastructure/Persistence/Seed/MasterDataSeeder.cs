using System.Linq;
using System.Threading.Tasks;
using ManufacturingERP.Application.Common.Interfaces;
using ManufacturingERP.Domain.Entities;
using ManufacturingERP.Infrastructure.Data; // your PublicDbContext namespace
using Microsoft.EntityFrameworkCore;

namespace ManufacturingERP.Infrastructure.Persistence.Seed
{
    public class MasterDataSeeder : IDataSeeder
    {
        private readonly PublicDbContext _context;

        public MasterDataSeeder(PublicDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedRawMaterials();
            await SeedSuppliers();
        }

        private async Task SeedRawMaterials()
        {
            var items = SeedData.RawMaterials();

            foreach (var item in items)
            {
                var exists = await _context.RawMaterialMasters
                    .AnyAsync(x => x.Name == item.Name);

                if (!exists)
                    _context.RawMaterialMasters.Add(item);
            }

            await _context.SaveChangesAsync();
        }

        private async Task SeedSuppliers()
        {
            var items = SeedData.Suppliers();

            foreach (var item in items)
            {
                var exists = await _context.Suppliers
                    .AnyAsync(x => x.Name == item.Name);

                if (!exists)
                    _context.Suppliers.Add(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}