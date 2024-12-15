using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Student_Grade_Management_System;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Pridedame sesijų palaikymą
builder.Services.AddDistributedMemoryCache(); // Sesijų duomenų saugojimui atmintyje
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sesijos galiojimo laikas
    options.Cookie.HttpOnly = true; // Apsauga nuo XSS
    options.Cookie.IsEssential = true; // Užtikrina, kad sesijos sausainis visada bus išsaugotas
});

builder.Services.AddDbContext<SystemDbContext>(options =>
{
    options.UseMySQL("Data Source=127.0.0.1;port=3306;Initial Catalog=tamo;User Id=root;Password=;SslMode=None;Convert Zero Datetime=True;");
});

var app = builder.Build();

// Konfigūruoti kalbą ir kultūrą
var supportedCultures = new[] { new CultureInfo("lt-LT"), new CultureInfo("en-US") };

// Nustatyti kalbos pasirinkimą
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("lt-LT"), // Nustatome, kad numatytoji kalba būtų lietuvių
    SupportedCultures = supportedCultures, // Sąrašas su palaikomomis kalbomis
    SupportedUICultures = supportedCultures // Palaikomos UI kultūros
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Įgaliname sesijas
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
