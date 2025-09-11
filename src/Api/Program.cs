using Api;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApi(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

app.MapControllers();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.Run();