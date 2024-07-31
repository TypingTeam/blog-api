using Keeper.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/", context => context.Response.WriteAsync("Hello from Keeper!"))
    .WithName("root")
    .WithOpenApi();

app.Run();
