using Budgetly.data;
using Budgetly.Extensions; // Para usar AddServices() DE TOTOS LOS SERVICIOS 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Inyectar el DbContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyectar servicios (modular desde ServiceRegistration.cs)
builder.Services.AddServices(); //  clase Extension

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Definir rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
