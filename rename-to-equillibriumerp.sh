#!/bin/bash

set -e

echo "🚀 Starting ManufacturingERP → EquillibriumERP rename..."

OLD_NAME="ManufacturingERP"
NEW_NAME="EquillibriumERP"

echo ""
echo "📦 Renaming project folders..."

[ -d "ManufacturingERP.Api" ] && mv ManufacturingERP.Api EquillibriumERP.Api
[ -d "ManufacturingERP.Application" ] && mv ManufacturingERP.Application EquillibriumERP.Application
[ -d "ManufacturingERP.Domain" ] && mv ManufacturingERP.Domain EquillibriumERP.Domain
[ -d "ManufacturingERP.Infrastructure" ] && mv ManufacturingERP.Infrastructure EquillibriumERP.Infrastructure

echo ""
echo "📄 Renaming csproj files..."

[ -f "EquillibriumERP.Api/ManufacturingERP.Api.csproj" ] && \
mv EquillibriumERP.Api/ManufacturingERP.Api.csproj \
EquillibriumERP.Api/EquillibriumERP.Api.csproj

[ -f "EquillibriumERP.Application/ManufacturingERP.Application.csproj" ] && \
mv EquillibriumERP.Application/ManufacturingERP.Application.csproj \
EquillibriumERP.Application/EquillibriumERP.Application.csproj

[ -f "EquillibriumERP.Domain/ManufacturingERP.Domain.csproj" ] && \
mv EquillibriumERP.Domain/ManufacturingERP.Domain.csproj \
EquillibriumERP.Domain/EquillibriumERP.Domain.csproj

[ -f "EquillibriumERP.Infrastructure/ManufacturingERP.Infrastructure.csproj" ] && \
mv EquillibriumERP.Infrastructure/ManufacturingERP.Infrastructure.csproj \
EquillibriumERP.Infrastructure/EquillibriumERP.Infrastructure.csproj

echo ""
echo "🧩 Renaming solution file..."

[ -f "ManufacturingERP.sln" ] && \
mv ManufacturingERP.sln EquillibriumERP.sln

echo ""
echo "🔍 Replacing namespaces and references..."

find . -type f \( \
-name "*.cs" -o \
-name "*.csproj" -o \
-name "*.json" -o \
-name "*.sln" -o \
-name "*.props" -o \
-name "*.targets" -o \
-name "*.yml" -o \
-name "*.yaml" \
\) -exec sed -i '' "s/$OLD_NAME/$NEW_NAME/g" {} +

echo ""
echo "🧹 Cleaning bin/obj folders..."

find . -type d \( -name bin -o -name obj \) -exec rm -rf {} +

echo ""
echo "🗑 Removing old EF migrations snapshots..."

find . -name "*ModelSnapshot.cs" -delete

echo ""
echo "📦 Restoring packages..."

dotnet restore

echo ""
echo "🏗 Building solution..."

dotnet build

echo ""
echo "✅ Rename completed successfully!"
echo ""
echo "Next recommended steps:"
echo ""
echo "1. Recreate EF migrations"
echo ""
echo "dotnet ef migrations add InitialCreate \\"
echo "--context MasterDbContext \\"
echo "--project EquillibriumERP.Infrastructure \\"
echo "--startup-project EquillibriumERP.Api"
echo ""
echo "2. Update database"
echo ""
echo "dotnet ef database update \\"
echo "--context MasterDbContext \\"
echo "--project EquillibriumERP.Infrastructure \\"
echo "--startup-project EquillibriumERP.Api"
echo ""
echo "3. Run API"
echo ""
echo "dotnet run --project EquillibriumERP.Api"