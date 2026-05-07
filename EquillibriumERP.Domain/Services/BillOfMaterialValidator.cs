using EquillibriumERP.Domain.Entities;

namespace EquillibriumERP.Domain.Services;

public static class BillOfMaterialValidator
{
    public static void Validate(BillOfMaterial bom)
    {
        if (bom.ProductId == Guid.Empty)
            throw new Exception("ProductId is required for BOM.");

        if (string.IsNullOrWhiteSpace(bom.Name))
            throw new Exception("BOM Name is required.");

        if (bom.Items == null || !bom.Items.Any())
            throw new Exception("BOM must have at least one item.");

        // Validate items
        foreach (var item in bom.Items)
        {
            if (item.RawMaterialId == Guid.Empty)
                throw new Exception("RawMaterialId is required.");

            if (item.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero.");

            if (item.WastagePercent < 0)
                throw new Exception("WastagePercent cannot be negative.");
        }

        // Prevent duplicate raw materials in same BOM
        var duplicates = bom.Items
            .GroupBy(x => x.RawMaterialId)
            .Where(g => g.Count() > 1)
            .ToList();

        if (duplicates.Any())
            throw new Exception("Duplicate RawMaterial entries found in BOM.");
    }
}