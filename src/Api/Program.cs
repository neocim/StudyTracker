using Api;
using Infrastructure;
using Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApi().AddApplication().AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapControllers();

app.Run();