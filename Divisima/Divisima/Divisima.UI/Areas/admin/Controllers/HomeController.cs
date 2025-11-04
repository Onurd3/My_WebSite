using System.Security.Claims;
using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.UI.Areas.admin.Controllers
{
	// [Area("admin")]
	[Area("panel"), Authorize] // bu kısım daha sonra yazılır ve program.cs içerisinde ek servisler gerektirir
	public class HomeController : Controller
	{
		IRepository<Admin> repoAdmin;
		public HomeController(IRepository<Admin> _repoAdmin)
		{
			repoAdmin = _repoAdmin;
		}
		public IActionResult Index()
		{
			return View();
		}

		// [AllowAnonymous] // Anonim girişlere izin ver dedik, böylece kilitli olan bu controllerda bu kısım kilitli olmaktan çıktı
		[AllowAnonymous, Route("admin/login")] // route ile bu sayfaya nasıl geleceğini anlattık
		public IActionResult Login(string ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}


		[AllowAnonymous, Route("admin/login"), HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
		{
			//...
			//. .Net Core Custom Authentication
			string md5Password = GeneralTools.GetMD5(password);
			Admin admin = repoAdmin.GetBy(x => x.UserName == username && x.Password == md5Password);
			if (admin != null) // MD5 metodundan sonra
			{
				List<Claim> claims = new List<Claim>
				{
					new Claim(ClaimTypes.PrimarySid, admin.ID.ToString()),
					new Claim(ClaimTypes.Name, admin.NameSurname)
				};
				ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "DivisimaAuth");
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties() { IsPersistent = true });

				// Bu derste eklendi
				if (!string.IsNullOrEmpty(ReturnUrl)) return Redirect(ReturnUrl);
				else return Redirect("/admin");
			}
			else TempData["bilgi"] = "Yanlış Kullanıcı Adı veya Parola";

			// önce yaz
			//if (repoAdmin.GetBy(x => x.UserName == username && x.Password == password) != null) // getMD5 metodunu git yaz. Çünkü kulanıcı normal değer girecek ama kayıt olurken şifresi md5 olarak kayıt oluyor.
			//{

			//}


			return RedirectToAction("Login");
		}

		[Route("admin/logout")] // program.cs içerisinde yeri zaten hazır idi
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}


	}
}
