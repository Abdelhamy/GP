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

	public class CoursesController : Controller
    {
		ICoursesService _CoursesService;
		ICourseRefServices _CourseRefServices;
		ISemesterCoursesServices _Semester;

		public CoursesController(ICoursesService CoursesService, ICourseRefServices CourseRefServices, ISemesterCoursesServices Semester)
		{
			this._CoursesService = CoursesService;
			this._CourseRefServices = CourseRefServices;
			this._Semester = Semester;
		}
		// GET: Administration/Courses
		public ActionResult Index()
        {
            ViewBag.CourseRefID = new SelectList(_CourseRefServices.ReadAllCoursesRef(), "CRefID", "SymbolMean");

            return View(_CoursesService.ReadAll());
        }
        public ActionResult Search(string CourseCode = null , string CourseName = null, int? CourseRefIDSelected = 0)
        {
            if (string.IsNullOrEmpty(CourseCode) && string.IsNullOrEmpty(CourseName) && CourseRefIDSelected == 0)
            {
                return PartialView("_index", _CoursesService.ReadAll());

            }
            else
            {
                if (CourseCode.Trim() == "") CourseCode = null; else CourseCode = CourseCode.Trim();
                if (CourseName.Trim() == "") CourseName = null; else CourseCode = CourseName.Trim();
                return PartialView("_index", _CoursesService.GetForSearch(CourseCode, CourseName, CourseRefIDSelected.Value));

            }
        }
        public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Cours course = _CoursesService.GetById(id.Value);
			if (course == null)
			{
				return HttpNotFound();
			}
			return View(course);
		}

		// GET: Administration/Cours/Create
		public PartialViewResult Create()
		{
			ViewBag.CourseRefID = new SelectList(_CourseRefServices.ReadAllCoursesRef(), "CRefID", "SymbolMean");
            ViewBag.CoursePrerequsiteID = new SelectList(_CoursesService.ReadAll(), "CourseID", "CourseCode");
			return PartialView("_Create");
		}

		// POST: Administration/Cours/Create?Cours
		[HttpPost]
		public ActionResult Create(Cours Cours)
		{
			if (ModelState.IsValid)
			{
                try
                {
                    int id = _CoursesService.AddOrUpdate(Cours);
                    if (id == 0) return View(Cours);
                    return RedirectToAction("Index");

                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
			}
			ViewBag.CourseRefID = new SelectList(_CourseRefServices.ReadAllCoursesRef(), "CRefID", "CRefSymbol");
			ViewBag.CoursePrerequsiteID = new SelectList(_CoursesService.ReadAll(), "CourseID", "CourseCode");
			return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

		// GET: Administration/Cours/Edit/{id}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Cours cours = _CoursesService.GetById(id.Value);
			if (cours == null)
			{
				return HttpNotFound();
			}
			ViewBag.CoursePrerequsiteID = new SelectList(_CoursesService.ReadAll(), "CourseID", "CourseCode" , cours.CoursePrerequsiteID);
			ViewBag.CourseRefID = new SelectList(_CourseRefServices.ReadAllCoursesRef(), "CRefID", "SymbolMean", cours.CourseRefID);

			return PartialView("_Edit",cours);
		}

		// POST: : Administration/Cours/Edit/{id}

		[HttpPost]
		public ActionResult Edit(Cours Cours)
		{
			if (ModelState.IsValid)
			{
                try
                {
                    int id = _CoursesService.AddOrUpdate(Cours);
                    if (id == 0) return View(Cours);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Administration/Cours/Delete/{id}
        public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Cours Cours = _CoursesService.GetById(id.Value);
			if (Cours == null)
			{
				return HttpNotFound();
			}
			Cours CoursChild = _CoursesService.GetChildCourses(id.Value);
			CoursesSemester sem = _Semester.GetTermsCourse(id.Value);
			if (CoursChild != null || sem != null)
			{
				return PartialView("_CannotDelete", Cours);

			}
			return PartialView("_Delete", Cours);
		}

		// POST: Administration/CourseRef/Delete/{id}
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Cours course = _CoursesService.GetById(id);
			_CoursesService.Delete(course);
			return RedirectToAction("Index");
		}
        public ActionResult OnSuccess()
        {
            return PartialView("_OnSuccess");
        }
        [HttpPost]
        public ActionResult OnSuccess(int? Done)
        {
            return RedirectToAction("Index");
        }

        public ActionResult OnFailure()
        {
            return PartialView("_OnFailure");
        }
        [HttpPost]
        public ActionResult OnFailure(int? Done)
        {
            return RedirectToAction("Index");
        }
    }


}