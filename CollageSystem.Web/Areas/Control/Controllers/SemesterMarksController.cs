using CollageSystem.Core;
using CollageSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CollageSystem.Web.Areas.Control.Controllers
{
    [Authorize(Roles = "Control")]
    public class SemesterMarksController : Controller
    {
        ISemestersServices _SemestersServices;
        IUsersServices _usersServices;
        ISemStudentCoursesServices _semStudentCoursesServices;
        ISemesterStudentServices _semesterStudentServices;

        public SemesterMarksController(ISemestersServices SemestersServices, IUsersServices usersServices, ISemStudentCoursesServices semStudentCoursesServices, ISemesterStudentServices semesterStudentServices)
        {
            _SemestersServices = SemestersServices;
            _usersServices = usersServices;
            _semStudentCoursesServices = semStudentCoursesServices;
            _semesterStudentServices = semesterStudentServices;
        }




        // GET: Control/SemesterMarks
        public ActionResult Index()
        {
            return View(_SemestersServices.ReadAll());
        }
        public ActionResult ShowSemesterStu(int? SemID)
        {
            Session["SemesterID"] = SemID.Value;
            IEnumerable<User> users = _usersServices.getSemesterUser(SemID.Value);

            return View(users);
        }
        public ActionResult SemesterCourses(int? StuId)
        {
            int SemID = (int)Session["SemesterID"];
            Session["StuID"] = StuId;
            SemesterStudent semstud = _semesterStudentServices.getSemesterStudId(StuId.Value, SemID);
            IEnumerable<SemStudentCours> courses = _semStudentCoursesServices.GetResultForSemester(semstud.SemStuID);

            return View(courses);
        }
        public ActionResult Edit(int? SemStuID, int? CourseID)
        {
            if (SemStuID == null || CourseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemStudentCours semStudentCours = _semStudentCoursesServices.getByid(CourseID.Value,SemStuID.Value);
            if (semStudentCours == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", semStudentCours);
        }


        [HttpPost]
        public ActionResult Edit(SemStudentCours semStudentCours)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (semStudentCours.FinalMarks + semStudentCours.MidTermMarks + semStudentCours.OralMarks + semStudentCours.PractiseMarks + semStudentCours.YearWorkMarks >= 60)
                    {
                        semStudentCours.passed = true;
                    }
                    int id = _semStudentCoursesServices.UpdateMarks(semStudentCours);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }

            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public ActionResult OnSuccess()
        {
            return PartialView("_OnSuccess");
        }
        [HttpPost]
        public ActionResult OnSuccess(int? Done)
        {
            return RedirectToAction("SemesterCourses", new { StuId = Session["StuID"] });
        }

        public ActionResult OnFailure()
        {
            return PartialView("_OnFailure");
        }
        [HttpPost]
        public ActionResult OnFailure(int? Done)
        {
            return RedirectToAction("SemesterCourses",new { StuId= Session["StuID"] });
        }

    }
}