//web app olu�turacak bir in�a edici al�yoruz
using Microsoft.EntityFrameworkCore;
using ProductApp.Extensions;
using Repositories.Contract;
using Repositories.EFCore;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container., View'larla birlikte controller kullanaca��z diyor, Servicler araac�l���yla kay�tlar yap�p ilerleyebiliyoruz
builder.Services.AddControllersWithViews();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program)); //Servic kayd� yapt�k. �oC ye automapper'i ekledik.
builder.Services.AddScoped<IProductRepository, ProductRepository>(); //her kullan�c� i�in bir kaynak olusturur. Singled bi tane kaynak olusturur herkes onu kullanir



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