using System.Linq;
using System.Threading.Tasks;
using EquillibriumERP.Application.Common.Interfaces;
using EquillibriumERP.Domain.Entities;
using EquillibriumERP.Infrastructure.Data; // your MasterDbContext namespace
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Infrastructure.Persistence.Seed
{
    public class MasterDataSeeder : IDataSeeder
    {
        private readonly MasterDbContext _context;

        public MasterDataSeeder(MasterDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedMasterRawMaterials();
            await SeedSuppliers();
        }

        private async Task SeedMasterRawMaterials()
        {
            var items = SeedData.MasterRawMaterials();

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