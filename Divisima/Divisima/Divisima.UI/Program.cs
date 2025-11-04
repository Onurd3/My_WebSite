using System.Security.Policy;
using Divisima.BL.Repositories;
using Divisima.DAL.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SQLContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
// Home daki veriyi index e taþýnmak zorunda kaldýðýmýz zaman bu servisi çaðýracaðýz
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
//
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
	opt.ExpireTimeSpan = TimeSpan.FromMinutes(60); // yaþam süresi 
	opt.LoginPath = "/admin/login"; // yetkisi olmadan giriþ yapmaya çalýþýrsa
	opt.LogoutPath = "/admin/logout"; // süre dolarsa nereye gitsin.
});
var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseStatusCodePagesWithRedirects("/hata/{0}");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(name: "dafault", pattern: "{controller=home}/{action=index}/{id?}");
app.Run();
