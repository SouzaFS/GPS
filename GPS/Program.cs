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
builder.Services
    .AddScoped<AppDBContext<UserModel>>()
    .AddScoped<AppDBContext<LocationModel>>()
    .AddScoped<IBaseRepository<UserModel>, BaseRepository<UserModel>>()
    .AddScoped<IBaseRepository<LocationModel>, BaseRepository<LocationModel>>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<ILocationService, LocationService>();

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
    .AddScoped<IUserQuery, UserQuery>()
    .AddScoped<ILocationMutation, LocationMutation>()
    .AddScoped<ILocationQuery, LocationQuery>();
    
builder.Services
    .AddGraphQLServer()
    .AddQueryType(a => a.Name("Query"))
        .AddType<UserQuery>()
        .AddType<LocationQuery>()
    .AddMutationType(a => a.Name("Mutation"))
        .AddType<UserMutation>()
        .AddType<LocationMutation>()
    .AddType<GraphQLModel<List<UserModel>>>()
    .AddType<GraphQLModel<List<LocationModel>>>()
    .AddType<GraphQLModel<UserModel>>()
    .AddType<GraphQLModel<LocationModel>>();

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
