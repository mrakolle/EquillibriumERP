#!/bin/bash

echo "🔄 Renaming PublicDbContext → MasterDbContext..."

find . -type f -name "*.cs" -exec sed -i '' 's/PublicDbContext/MasterDbContext/g' {} +
find . -type f -name "*.Designer.cs" -exec sed -i '' 's/PublicDbContext/MasterDbContext/g' {} +
find . -type f -name "*ModelSnapshot.cs" -exec sed -i '' 's/PublicDbContext/MasterDbContext/g' {} +

echo "🔍 Checking remaining references..."
grep -R "PublicDbContext" . || echo "🎉 No remaining references found"
