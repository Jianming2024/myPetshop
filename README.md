# myPetshop
5. In schema.sql
drop schema if exists petshop cascade;
create schema if not exists petshop;
create table petshop.seller (
                                id text primary key not null,
                                name text not null
);
create table petshop.pet (
    id text primary key not null,
    name text not null,
    breed text not null,
    createdAt timestamp with time zone not null,
    sold_date date default null,
    price numeric not null,
    seller text not null references petshop.seller(id)
);

6. Install the scaffolding CLI tool:
dotnet tool install -g dotnet-ef
7. Setup a .NET web project and install the required Nuget.
You can simply use the "dotnet new web" to create api and change the api.csproj file to the following:
<Project Sdk="Microsoft.NET.Sdk.Web">

    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.8">
            <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
            <PrivateAssets>all</PrivateAssets>
        </PackageReference>
        <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.4" />
    </ItemGroup>

</Project>

或者用commands
cd DataAccess
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Tools
From the solution root (petshop) run:
dotnet add Api reference DataAccess
8. Perform the scaffolding:
dotnet ef dbcontext scaffold "Host=ep-fancy-band-abpcz1nz-pooler.eu-west-2.aws.neon.tech; Database=neondb; Username=neondb_owner; Password=npg_vy7PfkNlLoB3; SSL Mode=VerifyFull; Channel Binding=Require;" Npgsql.EntityFrameworkCore.PostgreSQL   --context MyDbContext     --no-onconfiguring        --schema petshop   --force
9. Dependency injection + usage
Now add the scaffolded context class to dependency injection in Program.cs and enjoy usage (add in your .NET connection string to the Neon Postgres DB)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scaf;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(opts =>
{
    opts.UseNpgsql("YOUR CONNECTIONSTRING IN .NET FORMAT");
});

var app = builder.Build();

app.MapGet("/", ([FromServices] MyDbContext ctx) =>
{
    ctx.Sellers.Add(new Seller()
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Bob"
    });
    ctx.SaveChanges();
    return ctx.Sellers.ToList();
});

app.Run();

10. Protect the secret. 
My recommendation as of now is:
1.	Make a .gitignore with "dotnet new gitgnore" + add appsettings.json to the gitignore file.
2.	Then add the connection string to the appsettings.json with the name "Db" 
3. in Api/Program.cs
Reference this in your code like this:
builder.Configuration.GetValue<string>("Db")

Then start making:
•	A couple of endpoints (some CRUD logic with 2 Entities).
•	Some schema changes to the database and triggering a scaffold again.
<img width="468" height="638" alt="image" src="https://github.com/user-attachments/assets/331042e7-9fb0-4519-987b-33edfcbbedc8" />
