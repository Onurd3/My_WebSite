using System.Reflection.Metadata;
using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.UI.Areas.admin.Controllers
{
	[Area("admin"), Authorize]
	public class ProductPictureController : Controller
	{
		IRepository<ProductPicture> repoProductPicture;
        public ProductPictureController(IRepository<ProductPicture> _repoProductPicture)
        {
			repoProductPicture = _repoProductPicture;
        }
        public IActionResult Index(int productid)
		{
			ViewBag.ProductId = productid;

			return View(repoProductPicture.GetAll(x => x.ProductID == productid).OrderByDescending(x => x.ID));
		}

		public IActionResult New(int productid)
		{
			ViewBag.ProductId = productid;
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Insert(ProductPicture model)
		{
			if (ModelState.IsValid) // Gelen model Doğrulanmışsa
			{
				// Dosya ekleme işlemi aşamasında  -- Bu derste yazılacak
				if (Request.Form.Files.Any())
				{

					if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img","productPicture"))) // dosya yoksa bu scope da oluşturuyor
					{
						Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"));
					}
					string dosyaAdi = Request.Form.Files["Picture"].FileName;
					using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture",dosyaAdi), FileMode.Create))
					{
						await Request.Form.Files["Picture"].CopyToAsync(stream);
					}
					model.Picture = "/img/productPicture/" + dosyaAdi;
				}




				// Bu kod IRrepository Add metodundan sonra yazılsın
				repoProductPicture.Add(model);

				return RedirectToAction("Index", new { productid = model.ProductID });
			}
			else return RedirectToAction("New");
		}


		public ActionResult Edit(int id)
		{
			ProductPicture ProductPicture = repoProductPicture.GetBy(x => x.ID == id);
			if (ProductPicture != null) return View(ProductPicture);
			else return RedirectToAction("Index");
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ProductPicture model)
		{
			if (ModelState.IsValid) // Gelen model Doğrulanmışsa
			{
				// Dosya ekleme işlemi aşamasında  -- Bu derste yazılacak
				if (Request.Form.Files.Any())
				{

					if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"))) // dosya yoksa bu scope da oluşturuyor
					{
						Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture"));
					}
					string dosyaAdi = Request.Form.Files["Picture"].FileName;
					using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "productPicture", dosyaAdi), FileMode.Create))
					{
						await Request.Form.Files["Picture"].CopyToAsync(stream);
					}
					model.Picture = "/img/productPicture/" + dosyaAdi;
				}





				// Bu kod IRrepository Add metodundan sonra yazılsın
				repoProductPicture.Update(model);

				return RedirectToAction("Index", new { productid = model.ProductID });
			}
			else return RedirectToAction("New");
		}


		public IActionResult Delete(int id)
		{
			ProductPicture productPicture = repoProductPicture.GetBy(x => x.ID == id);
			if (productPicture != null)
			{
				if (!string.IsNullOrEmpty(productPicture.Picture))
				{
					string _pathFile = Directory.GetCurrentDirectory() + string.Format(@"\wwwroot") + productPicture.Picture.Replace("/", "\\");
					FileInfo fileInfo = new FileInfo(_pathFile);
					if (fileInfo.Exists) fileInfo.Delete();
				}
				repoProductPicture.Delete(productPicture);
			}

			
			return RedirectToAction("Index" , new { productid = productPicture.ProductID });
		}
	}
}
