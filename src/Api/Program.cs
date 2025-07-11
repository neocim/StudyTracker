using Api;
using Infrastructure;
using Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApi().AddApplication().AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.Run();