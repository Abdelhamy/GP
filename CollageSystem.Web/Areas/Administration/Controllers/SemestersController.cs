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

    public class SemestersController : Controller
    {
        ISemestersServices _SemestersServices;
        ISemNumbersServices _SemNumbersServices;
        IUsersServices _UsersServices;
        ISemesterStudentServices _semesterStudentServices;

        public SemestersController(ISemestersServices SemestersServices, ISemNumbersServices SemNumbersServices, IUsersServices UsersServices, ISemesterStudentServices semesterStudentServices)
        {
            _SemestersServices = SemestersServices;
            _SemNumbersServices = SemNumbersServices;
            _UsersServices = UsersServices;
            _semesterStudentServices = semesterStudentServices;
        }



        // GET: Administration/Semesters
        public ActionResult Index()
        {
            return View(_SemestersServices.ReadAll());
        }
        public ActionResult Create()
        {
            ViewBag.SemNumber = new SelectList(_SemNumbersServices.ReadAll(), "SemNumber1", "Desc");
            return PartialView("_Create");
        }
        [HttpPost]
        public ActionResult Create(Semester semester)
        {
            if (ModelState.IsValid)
            {
                _SemestersServices.AddOrUpdate(semester);
                //IEnumerable<User> users = _UsersServices.ReadAllStudent();
                //_semesterStudentServices.AddSemesterToStudents(semester.SemID, users);
                //_SemestersServices.AddSemesterToStudents(users, semID);
                return RedirectToAction("Index");
            }

            ViewBag.SemNumber = new SelectList(_SemNumbersServices.ReadAll(), "SemNumber1", "Desc");
            return PartialView("_Create", semester);
        }
        public ActionResult AddTermToStudent(int? SemID)
        {
            if (SemID != null)
            {
                IEnumerable<User> users = _UsersServices.ReadAllStudent();
                Semester semester = _SemestersServices.GetById(SemID.Value);
                _semesterStudentServices.AddSemesterToStudents(SemID.Value, users, semester);
                semester.Added = true;
                _SemestersServices.UpdateAdded(semester);
            }
            return RedirectToAction("Index");
        }
        // GET: Semesters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semester sem = _SemestersServices.GetById(id.Value);
            if (sem == null)
            {
                return HttpNotFound();
            }
            return View(sem);
        }
        // POST: Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Semester sem = _SemestersServices.GetById(id);
            _SemestersServices.Delete(sem);
            return RedirectToAction("Index");
        }
        public ActionResult DetailsOFTerm(int id)
        {
            Session["SemesterID"] = id;
            return Redirect("~/Administration/SemesterCourses/Index");

            //return RedirectToRoute("/Administration/SemesterCourses/Index?TermID=" + id);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semester semester = _SemestersServices.GetById(id.Value);
            if (semester == null)
            {
                return HttpNotFound();
            }

            ViewBag.SemNumber = new SelectList(_SemNumbersServices.ReadAll(), "SemNumber1", "Desc", semester.SemNumber);

            return PartialView("_Edit", semester);
        }
        // POST: : Administration/Cours/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Semester semester)
        {
            if (ModelState.IsValid)
            {
                int id = _SemestersServices.AddOrUpdate(semester);
                if (id == 0) return View(semester);
                return RedirectToAction("Index");
            }
            return View(semester);
        }

        public ActionResult ShowSemesterStu(int? SemID)
        {
            Session["SemesterID"] = SemID;
            return Redirect("~/Administration/SemesterCourses/ShowSemesterStu");
        }

    }
}