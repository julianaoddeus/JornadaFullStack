using Fina.Api;
using Fina.Api.Common.API;
using Fina.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddCongifuration();
builder.AddDbContexts();
builder.AddCorsOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();
app.Run();
