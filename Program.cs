using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using movie.Services.Implement;
using movie.Services.Interface;
using System.Data.SqlTypes;
using System.Security.AccessControl;
using System.Text;
using static movie.Services.Implement.VNpayservice;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserservice, Userservice>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes
        (builder.Configuration.GetSection("AppSettings:SecretKey").Value!))

    };
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{ 
x.AddSecurityDefinition("Auth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
{
    Description = "lam theo mau nay vd:bearer:{token}",
    Name = "Authorization",
    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

var serviceProvider = new ServiceCollection()
        .AddHttpContextAccessor()
        .BuildServiceProvider();

var vnpayService = serviceProvider.GetService<VNPayService>();
