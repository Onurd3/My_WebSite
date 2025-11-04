using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Divisima.UI.Areas.admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.UI.Areas.admin.Controllers
{
	[Area("admin")]
	public class CategoryController : Controller
	{
		IRepository<Category> repoCategory;
        public CategoryController(IRepository<Category> _repoCategory)

		{
			repoCategory = _repoCategory;
        }
        public IActionResult Index()
		{
			return View(repoCategory.GetAll().Include(x => x.ParentCategory).OrderByDescending(x => x.ID));
		}

		public IActionResult New()
		{
			CategoryVM categoryVM = new CategoryVM()
			{
				Category = new Category(),
				Categories = repoCategory.GetAll().OrderBy(x => x.Name)
			};
			return View(categoryVM);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Insert(CategoryVM model)
		{
			if (ModelState.IsValid)
			{
				repoCategory.Add(model.Category);
				return RedirectToAction("Index");
			}
			else return RedirectToAction("New");
		}

		public ActionResult Edit(int id)
		{
			Category category= repoCategory.GetBy(x => x.ID == id);
			CategoryVM categoryVM = new CategoryVM()
			{
				Category = category,
				Categories = repoCategory.GetAll().OrderBy(x => x.Name)
			};
			if (category != null) return View(categoryVM);
			else return RedirectToAction("Index");
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CategoryVM model)
		{

			if (ModelState.IsValid)
			{
				repoCategory.Update(model.Category);

				return RedirectToAction("Index");
			}
			else return RedirectToAction("New");
		}


		public IActionResult Delete(int id)
		{
			Category category = repoCategory.GetBy(x => x.ID == id);
			if (category != null) repoCategory.Delete(category);
			return RedirectToAction("Index");
		}
	}
}
