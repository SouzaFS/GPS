using GPS.DBContext;
using GPS.GraphQL;
using GPS.Models;
using GPS.Repositories;
using GPS.Repositories.Interfaces;
using GPS.Services;
using GPS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(options =>
{
    var configuration = builder.Configuration;
    string connectionString = configuration["ConnectionStrings:MongoDB"];
    var mongoClient = new MongoClient(connectionString);
    options.UseMongoDB(mongoClient, "gps_database");
});

builder.Services.AddScoped<IBaseRepository<UserModel>, BaseRepository<UserModel>>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
