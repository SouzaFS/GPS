using GPS.GraphQL.Services;
using GPS.GraphQL.Services.Interfaces;
using GPS.Models;
using GPS.Repositories;
using GPS.Repositories.Interfaces;
using GPS.REST.Services;
using GPS.REST.Services.Interfaces;
using GPS.DBContext;
using GPS.GraphQL.Controllers;

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
builder.Services
    .AddScoped<AppDBContext<UserModel>>()
    .AddScoped<IBaseRepository<UserModel>, BaseRepository<UserModel>>()
    .AddScoped<IUserService, UserService>();

if(builder.Environment.IsDevelopment()){
    
    builder.Services.AddCors(opt => {
        opt.AddPolicy("CorsPolicy", policy => {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
    });
}

if(builder.Environment.IsProduction()){
    
    builder.Services.AddCors(opt => {
        opt.AddPolicy("CorsPolicy", policy => {
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
        });
    });

    builder.WebHost.ConfigureKestrel(opt => {
        opt.ListenAnyIP(5000);
    });

    builder.Services.AddHealthChecks();
}

//GraphQL Scoped's
builder.Services
    .AddScoped<IUserMutation, UserMutation>()
    .AddScoped<IUserQuery, UserQuery>();
    
builder.Services
    .AddGraphQLServer()
    .AddQueryType(a => a.Name("Query"))
        .AddType<UsersQueryController>()
    .AddMutationType(a => a.Name("Mutation"))
        .AddType<UsersMutationController>()
    .AddType<GraphQL<List<UserModel>>>()
    .AddType<GraphQL<UserModel>>();

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

if (app.Environment.IsProduction()){
    
    app.UseHttpsRedirection();
    
    app.MapHealthChecks("/health");
}

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
