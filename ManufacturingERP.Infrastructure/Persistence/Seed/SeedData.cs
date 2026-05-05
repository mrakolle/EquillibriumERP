using System;
using System.Collections.Generic;
using System.Linq;
using ManufacturingERP.Domain.Entities;

namespace ManufacturingERP.Infrastructure.Persistence.Seed
{
    public static class SeedData
    {
        // =========================================================
        // 1. RAW MATERIALS (200 - GROUPED + CAS + FORMULA)
        // =========================================================
        public static List<RawMaterialMaster> RawMaterials()
        {
            var rnd = new Random(42);
            var materials = new List<RawMaterialMaster>();

            var groups = new Dictionary<string, (string[] names, string category)>
            {
                ["Base Surfactants"] = (new[] { "SLES 70%", "LABSA 96%", "AOS Powder", "SLS Powder" }, "Surfactant"),
                ["Secondary Surfactants"] = (new[] { "CAPB", "Amine Oxide", "Alcohol Ethoxylate 7EO" }, "Surfactant"),
                ["Builders & Fillers"] = (new[] { "Sodium Carbonate", "STPP", "Zeolite A", "Sodium Sulphate" }, "Builder"),
                ["Solvents"] = (new[] { "Ethanol", "Isopropanol", "Butyl Glycol", "Deionised Water" }, "Solvent"),
                ["Functional Additives"] = (new[] { "CMC", "EDTA", "Optical Brightener CBS-X" }, "Additive"),
                ["Enzymes"] = (new[] { "Protease", "Amylase", "Lipase" }, "Enzyme"),
                ["Bleaching Systems"] = (new[] { "Sodium Hypochlorite", "Hydrogen Peroxide 35%" }, "Bleach"),
                ["Fragrance & Dyes"] = (new[] { "Lemon Fragrance", "Lavender Fragrance", "Blue Dye" }, "Fragrance")
            };

            var formulas = new Dictionary<string, (string formula, string cas)>
            {
                ["SLES 70%"] = ("C12H25(OCH2CH2)nOSO3Na", "9004-82-4"),
                ["LABSA 96%"] = ("C18H30O3S", "27176-87-0"),
                ["Sodium Carbonate"] = ("Na2CO3", "497-19-8"),
                ["Ethanol"] = ("C2H6O", "64-17-5"),
                ["Hydrogen Peroxide 35%"] = ("H2O2", "7722-84-1"),
                ["Sodium Hypochlorite"] = ("NaOCl", "7681-52-9"),
                ["EDTA"] = ("C10H16N2O8", "60-00-4")
            };

            int counter = 1;

            foreach (var group in groups)
            {
                for (int i = 0; i < 25; i++)
                {
                    var baseName = group.Value.names[rnd.Next(group.Value.names.Length)];

                    formulas.TryGetValue(baseName, out var chem);

                    materials.Add(new RawMaterialMaster
                    {
                        Id = Guid.NewGuid(),
                        Name = $"{baseName} Batch {counter++}",
                        Category = group.Key,
                        ChemicalFormula = chem.formula ?? "N/A",

                        // CAS ADDED BACK (this was missing before)
                        CASNumber = chem.cas ?? "N/A",

                        PurityMin = (decimal)(85 + rnd.NextDouble() * 5),
                        PurityMax = (decimal)(90 + rnd.NextDouble() * 9),
                        Grade = group.Value.category,
                        UnitOfMeasure = "kg"
                    });
                }
            }

            if (materials.Count != 200)
                throw new Exception($"RawMaterials count incorrect: {materials.Count}");

            return materials;
        }

        // =========================================================
        // 2. SUPPLIERS (100 - SOUTH AFRICA)
        // =========================================================
        public static List<Supplier> Suppliers()
        {
            var suppliers = new List<Supplier>
            {
                new() { Id = Guid.NewGuid(), Name = "EzeeChem", Email="sales@ezeechem.co.za", Phone="0115550101", RegistrationNumber="2012/123456/07" },
                new() { Id = Guid.NewGuid(), Name = "Raw Detergent Supplies", Email="info@rawdetergent.co.za", Phone="0115557788", RegistrationNumber="2015/456789/07" },
                new() { Id = Guid.NewGuid(), Name = "Nabane SA", Email="sales@nabanesa.co.za", Phone="0115558899", RegistrationNumber="2018/654321/07" },

                new() { Id = Guid.NewGuid(), Name = "AECI Chemicals", Email="info@aeci.co.za", Phone="0118068700", RegistrationNumber="1924/002590/06" },
                new() { Id = Guid.NewGuid(), Name = "Brenntag South Africa", Email="info@brenntag.co.za", Phone="0317928000", RegistrationNumber="1999/000111/07" },
                new() { Id = Guid.NewGuid(), Name = "Omnia Specialities", Email="info@omnia.co.za", Phone="0169607000", RegistrationNumber="1967/000333/06" },
                new() { Id = Guid.NewGuid(), Name = "Protea Chemicals", Email="sales@prochems.co.za", Phone="0113932000", RegistrationNumber="1974/000222/06" },
                new() { Id = Guid.NewGuid(), Name = "Chemfit SA", Email="info@chemfit.co.za", Phone="0215553000", RegistrationNumber="2010/445566/07" }
            };

            string[] cities =
            {
                "Midrand","Midvaal","Vereeniging","Pretoria","Germiston",
                "Kempton Park","Sandton","Randburg","Roodepoort","Benoni",
                "Boksburg","Alberton","Krugersdorp","Soweto"
            };

            string[] types =
            {
                "Chemical Supplies","Industrial Chemicals","Bulk Chemicals",
                "Detergent Raw Materials","Chemical Distributors"
            };

            int i = 1;

            while (suppliers.Count < 100)
            {
                suppliers.Add(new Supplier
                {
                    Id = Guid.NewGuid(),
                    Name = $"{cities[i % cities.Length]} {types[i % types.Length]} {i}",
                    Email = $"sales{i}@supplier{i}.co.za",
                    Phone = $"011{(5000000 + i):0000000}",
                    RegistrationNumber = $"201{(i % 10)}/123{i:000}/07"
                });

                i++;
            }

            return suppliers;
        }

        // =========================================================
        // 3. LINK RAW MATERIALS ↔ SUPPLIERS
        // =========================================================
        public static List<SupplierRawMaterial> RawMaterialSuppliers(
            List<RawMaterialMaster> materials,
            List<Supplier> suppliers)
        {
            var rnd = new Random(99);
            var mappings = new List<SupplierRawMaterial>();

            foreach (var material in materials)
            {
                int supplierCount =
                    material.Category.Contains("Surfactant") ? 5 :
                    material.Category.Contains("Enzyme") ? 2 :
                    4;

                var selected = suppliers
                    .OrderBy(x => rnd.Next())
                    .Take(supplierCount);

                foreach (var supplier in selected)
                {
                    mappings.Add(new SupplierRawMaterial
                    {
                        SupplierId = supplier.Id,
                        RawMaterialMasterId = material.Id,
                        PricePerKg = (decimal)(10 + rnd.NextDouble() * 120),
                        LeadTimeDays = rnd.Next(2, 10).ToString()
                    });
                }
            }

            return mappings;
        }
    }
}