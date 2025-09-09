using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

/*var connStr = builder.Configuration.GetConnectionString("Db");
if (string.IsNullOrWhiteSpace(connStr))
    throw new InvalidOperationException("ConnectionStrings:Db is missing.");*/

builder.Services.AddDbContext<MyDbContext>(opts =>
{
    //opts.UseNpgsql("Db");
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Db"));
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