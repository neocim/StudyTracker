using Api;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPresentation().AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapControllers();

app.Run();