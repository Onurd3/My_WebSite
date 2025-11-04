using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.UI.Controllers
{
	public class HomeController : Controller
	{


		#region BURASI İLK YAPILACAK
		//IRepository<Admin> repoAdmin;
		//public HomeController(IRepository<Admin> _repoAdmin)
		//{
		//	repoAdmin = _repoAdmin;
		//}
		//public IActionResult Index()
		//{

		//	return View(repoAdmin.GetBy(x => x.ID == 1));
		//}
		#endregion




		#region SONRADAN YAPILACAK KISIM

		//IRepository<Slide> repoSlide;
		//public HomeController(IRepository<Slide> _repoSlide)
		//{
		//	repoSlide = _repoSlide;
		//}
		//public IActionResult Index()
		//{

		//	return View(repoSlide.GetAll());
		//}
		#endregion

		#region BURASI KAPALI KALSIN
		// veriyi test ettikten sonra git admin tarafında giriş için kodları yaz
		//public IActionResult Index()
		//{
		//	return View();
		//}
		#endregion




		// Bu derste

		IRepository<Product> repoProduct;
		IRepository<Slide> repoSlide;
		public HomeController(IRepository<Slide> _repoSlide, IRepository<Product> _repoProduct)
		{
			repoSlide = _repoSlide;
			repoProduct = _repoProduct;	
		}
		public IActionResult Index()
		{
			// 18. DERS COOKİE OLUŞTURMA
			//CookieOptions cookie = new CookieOptions();
			//cookie.Expires = DateTime.Now.AddHours(10);
			//Response.Cookies.Append("C1", "Infotech Academy", cookie);




			IndexVM indexVM = new IndexVM()
			{
				Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
				LatestProducts = repoProduct.GetAll().Include(x => x.ProductPictures).OrderByDescending(x => x.ID).Take(8),
				SalesProducts = repoProduct.GetAll().Include(x => x.ProductPictures).OrderBy(x => Guid.NewGuid()).Take(8)
			};
			return View(indexVM);
		}
	}
}


#region TARİF

#endregion
