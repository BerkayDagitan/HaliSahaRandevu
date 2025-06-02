using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Token;
using BusinessLayer.Services.ApiServices;
using BusinessLayer.Services.PasswordHash;
using BusinessLayer.Services.TokenServices;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectContext>

    (
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddHttpClient<IUserApiServices, UserApiServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5138/api/");
});

builder.Services.AddHttpClient<IAppointmentApiServices, AppointmentApiServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5138/api/");
});

builder.Services.AddHttpClient<IPitchApiServices, PitchApiServices>();

builder.Services.AddHttpClient<ICityApiServices, CityApiServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
