#!/bin/bash
dotnet ef dbcontext scaffold \
  "Server=localhost;Database=testdb;User Id=testuser;Password=testpass;" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir Models \
  --context-dir . \
  --context MyDbContext  \
  --no-onconfiguring \
  --data-annotations \
  --force
  
#  Make sure DataAccess.csproj references these packages first  
#  dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
#  dotnet add package Microsoft.EntityFrameworkCore.Tools