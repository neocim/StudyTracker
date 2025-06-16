using Api;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApi().AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapControllers();

app.Run();