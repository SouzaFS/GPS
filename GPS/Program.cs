using GPS.GraphQL.Interfaces;
using GPS.GraphQL;
using GPS.Models;
using GPS.Repositories;
using GPS.Repositories.Interfaces;
using GPS.Services;
using GPS.Services.Interfaces;
using GPS.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MongoDB Configuration for GraphQL and REST API
builder.Services.Configure<DBSettings>(
    builder.Configuration.GetSection("Database"));

//REST API Scoped's
builder.Services.AddScoped<AppDBContext<UserModel>>();
builder.Services.AddScoped<IBaseRepository<UserModel>, BaseRepository<UserModel>>();
builder.Services.AddScoped<IUserService, UserService>();


//GraphQL Scoped's
builder.Services.AddScoped<IUserMutation, UserMutation>();
builder.Services.AddScoped<IUserQuery, UserQuery>();
builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
        .AddTypeExtension<UserQuery>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddTypeExtension<UserMutation>()
    .AddSorting()
    .AddFiltering();

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
