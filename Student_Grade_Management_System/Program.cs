using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Student_Grade_Management_System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SystemDbContext>(options =>
{
    options.UseMySQL("Data Source=127.0.0.1;port=3306;Initial Catalog=tamo;User Id=root;Password=;SslMode=None;Convert Zero Datetime=True;");
});

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
