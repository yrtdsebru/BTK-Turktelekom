//web app oluþturacak bir inþa edici alýyoruz
using Microsoft.EntityFrameworkCore;
using ProductApp.Extensions;
using Repositories.Contract;
using Repositories.EFCore;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container., View'larla birlikte controller kullanacaðýz diyor, Servicler araacýlýðýyla kayýtlar yapýp ilerleyebiliyoruz
builder.Services.AddControllersWithViews();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program)); //Servic kaydý yaptýk. ÝoC ye automapper'i ekledik.
builder.Services.AddScoped<IProductRepository, ProductRepository>(); //her kullanýcý için bir kaynak olusturur. Singled bi tane kaynak olusturur herkes onu kullanir



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});



app.Run();