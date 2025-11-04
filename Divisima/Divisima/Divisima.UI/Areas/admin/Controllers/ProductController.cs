using System.Reflection.Metadata;
using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Areas.admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.UI.Areas.admin.Controllers
{
	[Area("admin"), Authorize]
	public class ProductController : Controller
	{
		IRepository<Product> repoProduct;

		IRepository<Brand> repoBrand;
		IRepository<Category> repoCategory;

		public ProductController(IRepository<Product> _repoProduct, IRepository<Brand> _repoBrand, IRepository<Category> _repoCategory)
        {
			repoProduct = _repoProduct;
			repoBrand = _repoBrand;
			repoCategory = _repoCategory;
        }
        public IActionResult Index()
		{
			return View(repoProduct.GetAll().OrderByDescending(x => x.ID));
		}

		public IActionResult New()
		{
			ProductVM productVM = new ProductVM
			{
				Product = new Product(),
				Brands = repoBrand.GetAll().OrderBy(x => x.Name),
				Categories = repoCategory.GetAll(x => x.ParentID!=null).OrderBy(x => x.Name)
			};
			return View(productVM);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public  IActionResult Insert(ProductVM model)
		{
			if (ModelState.IsValid)
			{
				repoProduct.Add(model.Product);

				return RedirectToAction("Index");
			}
			else return RedirectToAction("New");
		}


		public ActionResult Edit(int productid)
		{
			Product product = repoProduct.GetBy(x => x.ID == productid);
			ProductVM productVM = new ProductVM
			{
				Product = product,
				Brands = repoBrand.GetAll().OrderBy(x => x.Name),
				Categories = repoCategory.GetAll(x => x.ParentID != null).OrderBy(x => x.Name)
			};
			if (product != null) return View(productVM);
			else return RedirectToAction("Index");
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Edit(ProductVM model)
		{
			if (ModelState.IsValid) 
			{
				repoProduct.Update(model.Product);

				return RedirectToAction("Index");
			}
			else return RedirectToAction("New");
		}


		public IActionResult Delete(int id)
		{
			Product Product = repoProduct.GetBy(x => x.ID == id);
			if (Product != null) repoProduct.Delete(Product);
			return RedirectToAction("Index");
		}
	}
}
