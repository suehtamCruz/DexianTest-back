using DexianTest_back.Models;
using DexianTest_back.Services;
using DexianTest_back.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DefaultCorsPolicy",
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin();
                      });
});
builder.Services.Configure<DataBaseModel>(
   builder.Configuration.GetSection("MongoDbConnection"));

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
var app = builder.Build();


app.UseCors("DefaultCorsPolicy");

app.UseSwagger();
app.UseSwaggerUI(); 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
