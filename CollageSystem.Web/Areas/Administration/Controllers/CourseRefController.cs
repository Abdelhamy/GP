using CollageSystem.Core;
using CollageSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CollageSystem.Web.Areas.Administration.Controllers
{
	[Authorize(Roles = "Admin")]

	public class CourseRefController : Controller
    {
		ICourseRefServices _CourseRefServices;
		ICoursesService _CoursesService;

		public CourseRefController(ICourseRefServices CourseRefServices, ICoursesService CoursesService)
		{
			this._CourseRefServices = CourseRefServices;
			this._CoursesService = CoursesService;
		}
		
		// GET: Administration/CourseRef
		public PartialViewResult Index()
        {
			return PartialView("_Index",_CourseRefServices.ReadAllCoursesRef());
		}

		// GET: Administration/CourseRef/Details/{id}
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CourseRef courseRef = _CourseRefServices.GetById(id.Value);
			if (courseRef == null)
			{
				return HttpNotFound();
			}
			return View(courseRef);
		}

		// GET: Administration/CourseRef/Create
		public PartialViewResult Create()
		{
			return PartialView("_Create");
		}

		// POST: Administration/CourseRef/Create?courseRef
		[HttpPost]	
		public ActionResult Create(CourseRef courseRef)
		{
			if (ModelState.IsValid)
			{
				int id = _CourseRefServices.AddOrUpdate(courseRef);
				if(id == 0) return View(courseRef);
				return RedirectToAction("Index", "Courses");
			}

			return RedirectToAction("Index", "Courses");
		}

		// GET: Administration/CourseRef/Edit/{id}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CourseRef courseRef = _CourseRefServices.GetById(id.Value);
			if (courseRef == null)
			{
				return HttpNotFound();
			}
			return PartialView("_Edit",courseRef);
		}

		// POST: : Administration/CourseRef/Edit/{id}

		[HttpPost]
		public ActionResult Edit( CourseRef courseRef)
		{
			if (ModelState.IsValid)
			{
				int id = _CourseRefServices.AddOrUpdate(courseRef);
				if (id == 0) return View(courseRef);
				return RedirectToAction("Index", "Courses");
			}
			return RedirectToAction("Index", "Courses");
		}

		// GET: Administration/CourseRef/Delete/{id}
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CourseRef courseRef = _CourseRefServices.GetById(id.Value);
			if (courseRef == null)
			{
				return HttpNotFound();
			}
			Cours course = _CoursesService.GetCourseForRef(id.Value);
			if (course != null)
			{
				return PartialView("_CannotDelete", courseRef);

			}
			return PartialView("_Delete",courseRef);
		}

		// POST: Administration/CourseRef/Delete/{id}
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			CourseRef courseRef = _CourseRefServices.GetById(id);
			_CourseRefServices.Delete(courseRef);
			return RedirectToAction("Index" , "Courses");
		}
	}
}