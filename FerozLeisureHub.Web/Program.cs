using FerozLeisureHub.Application;
using FerozLeisureHub.Domain.Entities;
using FerozLeisureHub.Insfrastructure;
using FerozLeisureHub.Insfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FerozLeisureHub.Insfrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()

.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddApplicationServices();
builder.Services.ConfigureApplicationCookie(option=>{
    option.AccessDeniedPath ="/Account/AccessDenied";
     option.LoginPath="/Account/Login";
});
builder.Services.Configure<IdentityOptions>(option=>{
    option.Password.RequiredLength=6;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

