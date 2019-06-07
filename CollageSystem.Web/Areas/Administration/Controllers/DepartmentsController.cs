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

	public class DepartmentsController : Controller
    {

		IDepartmentsServices _DepartmentsServices;
		IUsersServices _IUsersServices;
		ISemesterCoursesServices _Semester;


		public DepartmentsController(IDepartmentsServices DepartmentsServices, IUsersServices IUsersServices, ISemesterCoursesServices Semester)
		{
			this._DepartmentsServices = DepartmentsServices;
			this._IUsersServices = IUsersServices;
			this._Semester = Semester;
		}
		// GET: Administration/Departments
		public ActionResult Index()
        {
            return View(_DepartmentsServices.ReadAll());
        }
		

		// GET: Administration/Department/Create
		public ActionResult Create()
		{
			ViewBag.DepManegerID = new SelectList(_IUsersServices.ReadAllInstructor(), "ID", "FirstName");

			return PartialView("_Create");
		}

		// POST: Administration/Department/Create?Department
		[HttpPost]
		public ActionResult Create(Department Department)
		{
			if (ModelState.IsValid)
			{
				int id = _DepartmentsServices.AddOrUpdate(Department);
				if (id == 0) return View(Department);
				return RedirectToAction("Index");
			}
			ViewBag.DepManegerID = new SelectList(_IUsersServices.ReadAllInstructor(), "ID", "FirstName");
			
			return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

		// GET: Administration/Department/Edit/{id}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Department Department = _DepartmentsServices.GetById(id.Value);
			if (Department == null)
			{
				return HttpNotFound();
			}
			ViewBag.DepManegerID = new SelectList(_IUsersServices.ReadAllInstructor(), "ID", "FirstName", Department.DepManegerID);

			return PartialView("_Edit", Department);
		}

		// POST: : Administration/Department/Edit/{id}

		[HttpPost]
		public ActionResult Edit(Department Department)
		{
			if (ModelState.IsValid)
			{
				int id = _DepartmentsServices.AddOrUpdate(Department);
				if (id == 0) return View(Department);
				return RedirectToAction("Index");
			}
			ViewBag.DepManegerID = new SelectList(_IUsersServices.ReadAllInstructor(), "ID", "FirstName", Department.DepManegerID);

			return PartialView("_Edit", Department);
		}

		// GET: Administration/Department/Delete/{id}
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Department Department = _DepartmentsServices.GetById(id.Value);
			if (Department == null)
			{
				return HttpNotFound();
			}
			ViewBag.DepManegerID = new SelectList(_IUsersServices.ReadAllInstructor(), "ID", "FirstName", Department.DepManegerID);
			User user = _IUsersServices.GetUsersDepartment(id.Value);
			CoursesSemester sem = _Semester.GetTermsDepartemt(id.Value);

			if (user != null || sem != null)
			{
				return PartialView("_CannotDelete", Department);

			}
			return PartialView("_Delete", Department);

		}

		// POST: Administration/DepartmenteRef/Delete/{id}
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Department Departmente = _DepartmentsServices.GetById(id);
			_DepartmentsServices.Delete(Departmente);
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
