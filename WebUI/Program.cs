 using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Token;
using BusinessLayer.Services.ApiServices;
using BusinessLayer.Services.TokenServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IUserApiServices, UserApiServices>();
builder.Services.AddScoped<IAppointmentApiServices, AppointmentApiServices>();
builder.Services.AddScoped<IPitchApiServices, PitchApiServices>();
builder.Services.AddScoped<ICityApiServices, CityApiServices>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginPage}/{action=Login}/{id?}");

app.Run();
