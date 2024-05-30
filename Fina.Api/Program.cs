using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = "server=localhost,1433;Database=Fina;User ID=SA;Password=1q2w3e4r@#$;TrustServerCertificate=True";

builder.Services.AddDbContext<AppDbContext>( x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
