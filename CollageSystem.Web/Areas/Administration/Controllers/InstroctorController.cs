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

	public class InstroctorController : Controller
    {
		IUsersServices _UsersServices;
		IDepartmentsServices _DepartmentsServicesvices;

		public InstroctorController(IUsersServices UsersServices, IDepartmentsServices DepartmentsServicesvices)
		{
			this._UsersServices = UsersServices;
			this._DepartmentsServicesvices = DepartmentsServicesvices;

		}
		// GET: Administration/Instroctor
		public ActionResult Index()
        {
            return View(_UsersServices.ReadAllInstructor().OrderBy(x => x.DepartmentID));
        }
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User User = _UsersServices.GetById(id.Value);
			if (User == null)
			{
				return HttpNotFound();
			}
			return View(User);
		}

		// GET: Administration/User/Create
		public ActionResult Create()
		{
			ViewBag.DepartmentID = new SelectList(_DepartmentsServicesvices.ReadAll(), "DepID", "DepName");
			//ViewBag.PermissionID = new SelectList(db.Permissions, "PermissionID", "PermissionRole", user.PermissionID);
			return PartialView("_Create");
		}

		// POST: Administration/User/Create?User
		[HttpPost]
		public ActionResult Create(User User)
		{
			if (ModelState.IsValid)
			{
				User.PermissionID = 3;
				int added = _UsersServices.AddOrUpdate(User);
				if (added == 0) return View(User);
				return RedirectToAction("Index");
			}
			ViewBag.DepartmentID = new SelectList(_DepartmentsServicesvices.ReadAll(), "DepID", "DepName");

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

		// GET: Administration/User/Edit/{id}

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User User = _UsersServices.GetById(id.Value);
			if (User == null)
			{
				return HttpNotFound();
			}
			ViewBag.DepartmentID = new SelectList(_DepartmentsServicesvices.ReadAll(), "DepID", "DepName",User.DepartmentID);

			return PartialView("_Edit", User);
		}

		// POST: : Administration/User/Edit/{id}

		[HttpPost]
		public ActionResult Edit(User User)
		{
			if (ModelState.IsValid)
			{
				User.PermissionID = 3;
				int updated = _UsersServices.AddOrUpdate(User);
				if (updated == 0) return View(User);
				return RedirectToAction("Index");
			}
			ViewBag.DepartmentID = new SelectList(_DepartmentsServicesvices.ReadAll(), "DepID", "DepName");

			return PartialView("_Edit", User);
		}

		// GET: Administration/User/Delete/{id}
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			User User = _UsersServices.GetById(id.Value);
			if (User == null)
			{
				return HttpNotFound();
			}
			return PartialView("_Delete", User);
		}

		// POST: Administration/UsereRef/Delete/{id}
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			User Usere = _UsersServices.GetById(id);
			_UsersServices.Delete(Usere);
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