//web app olu�turacak bir in�a edici al�yoruz
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container., View'larla birlikte controller kullanaca��z diyor, Servicler araac�l���yla kay�tlar yap�p ilerleyebiliyoruz
builder.Services.AddControllersWithViews();

//servise kay�t yapt�k inversion of control(IoC), S�rtlad���m frameworklerin ak���n�n aras�na giriyoruz. Biri sendden context isterse sen repository context g�nder
//IoC �nemli ad�mlar =>kay�t(register), ��zme(solve), Dispose(Lifecycle)[max cycle'� y�netme].
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
