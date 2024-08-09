using Keeper.API.Infrastructure;
using Keeper.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddTransient<PostsHandler>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/post/{postSlug}", (string postSlug, PostsHandler handler, CancellationToken token) 
    => handler.GetPostBySlugAsync(postSlug, token));

app.UseHttpsRedirection();
app.MapGet("/", context => context.Response.WriteAsync("Hello from Keeper!"))
    .WithName("root")
    .WithOpenApi();

app.Run();
