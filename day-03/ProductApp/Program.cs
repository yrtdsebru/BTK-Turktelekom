//web app oluþturacak bir inþa edici alýyoruz
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container., View'larla birlikte controller kullanacaðýz diyor, Servicler araacýlýðýyla kayýtlar yapýp ilerleyebiliyoruz
builder.Services.AddControllersWithViews();

//servise kayýt yaptýk inversion of control(IoC), Sýrtladýðým frameworklerin akýþýnýn arasýna giriyoruz. Biri sendden context isterse sen repository context gönder
//IoC önemli adýmlar =>kayýt(register), çözme(solve), Dispose(Lifecycle)[max cycle'ý yönetme].
builder.Services.AddDbContext<RepositoryContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection")));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
