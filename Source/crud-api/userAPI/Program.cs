using System.Data;
using System.Data.SqlClient;
using userAPI;
using userAPI.Core;
using userAPI.Core.Interface;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:5173"
                                              ).AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .AllowCredentials()
                                 .WithHeaders("Content-Type");
                      });
});

string dbConnectionString = await AWSSecretManagerConfiguration.GetSecret();
builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

// Add services to the container.
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

