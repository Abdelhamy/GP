using CollageSystem.Core;
using CollageSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollageSystem.Web.Areas.Administration.Controllers
{
	[Authorize(Roles = "Admin")]

	public class SemesterCoursesController : Controller
	{
		IDepartmentsServices _DepartmentsServices;
		ICoursesService _coursesService;
		ISemesterCoursesServices _semesterCoursesSevices;
        ICourseRefServices _courseRefServices;
        ISemesterStudentServices _semesterStudentServices;
        IUsersServices _usersServices;
        ISemStudentCoursesServices _semStudentCoursesServices;

        public SemesterCoursesController(IDepartmentsServices DepartmentsServices, ICoursesService coursesService, ISemesterCoursesServices semesterCoursesSevices, ICourseRefServices courseRefServices, ISemesterStudentServices semesterStudentServices, IUsersServices usersServices, ISemStudentCoursesServices semStudentCoursesServices)
        {
            _DepartmentsServices = DepartmentsServices;
            _coursesService = coursesService;
            _semesterCoursesSevices = semesterCoursesSevices;
            _courseRefServices = courseRefServices;
            _semesterStudentServices = semesterStudentServices;
            _usersServices = usersServices;
            _semStudentCoursesServices = semStudentCoursesServices;
        }


        // GET: Administration/SemesterCourses
        public ActionResult Index(int? DepID)
		{
			Session["DepID"] = DepID;

			if (DepID == null)
			{
				ViewBag.Departments = new SelectList(_DepartmentsServices.ReadAll(), "DepID", "DepName", 0);

			}
			else
			{
				ViewBag.Departments = new SelectList(_DepartmentsServices.ReadAll(), "DepID", "DepName", DepID.Value);

			}

			if (DepID == null || Session["SemesterID"] == null)
				return View();
			else
			{

				int Depid = DepID.Value;
				int SemesterID = int.Parse(Session["SemesterID"].ToString());

				IEnumerable<Cours> courses = _coursesService.GetCousrsDepartmentTerm(Depid, SemesterID);
				return View(courses);
			}


		}
        public ActionResult Search(string CourseCode = null, string CourseName = null , int? CourseRefIDSelected = 0)
        {
            int Depid = (int)Session["DepID"];
            int SemesterID = int.Parse(Session["SemesterID"].ToString());
            List<Cours> indexes = new List<Cours>();
            List<Cours> coursesDep = _coursesService.GetCousrsDepartmentTerm(Depid, SemesterID).ToList();
            List<Cours> Allcourses = new List<Cours>();
            if (string.IsNullOrEmpty(CourseCode) && string.IsNullOrEmpty(CourseName) && CourseRefIDSelected == 0)
            {         
                Allcourses = _coursesService.ReadAll().ToList();
            }
            else
            {
                Allcourses = _coursesService.GetForSearch(CourseCode, CourseName, CourseRefIDSelected.Value).ToList();
            }
            foreach (var item in Allcourses)
            {
                foreach (var itemDep in coursesDep)
                {
                    if (item.CourseID == itemDep.CourseID)
                    {
                        indexes.Add(item);
                    }
                }

            }
            foreach (var item in indexes)
            {
                Allcourses.Remove(item);

            }
            return PartialView("_Search", Allcourses);
        }
        public ActionResult CoursesForDepartment(int? DepID)
		{
            if (DepID == null || Session["SemesterID"] == null)
                return View("Search", null);
            else
            {

                int Depid = DepID.Value;
                int SemesterID = int.Parse(Session["SemesterID"].ToString());
                List<Cours> coursesDep = _coursesService.GetCousrsDepartmentTerm(Depid, SemesterID).ToList();
                List<Cours> Allcourses = _coursesService.ReadAll().ToList();
                List<Cours> indexes = new List<Cours>();

                foreach (var item in Allcourses)
                {
                    foreach (var itemDep in coursesDep)
                    {
                        if (item.CourseID == itemDep.CourseID)
                        {
                            indexes.Add(item);
                        }
                    }

                }
                foreach (var item in indexes)
                {
                    Allcourses.Remove(item);

                }
                ViewBag.CourseRefID = new SelectList(_courseRefServices.ReadAllCoursesRef(), "CRefID", "SymbolMean");
                return View("Search", Allcourses);
            }
            
		}
		public ActionResult AddCourseToTerm(int? CourseID)
		{
			int DepID = 0;
			if (CourseID != null)
			{
				DepID = int.Parse(Session["DepID"].ToString());
				int SemesterID = int.Parse(Session["SemesterID"].ToString());
				int Courseid = CourseID.Value;
				_semesterCoursesSevices.Add(new CoursesSemester { CourseID = Courseid, SemID = SemesterID, DepartmentID = DepID });
			}

			return RedirectToAction("CoursesForDepartment", new { DepID = DepID } );
		}
		public ActionResult Delete(int? CourseID)
		{
			int DepID = 0;
			if (CourseID != null)
			{
				DepID = int.Parse(Session["DepID"].ToString());
				int SemesterID = int.Parse(Session["SemesterID"].ToString());
				int Courseid = CourseID.Value;
				CoursesSemester s = _semesterCoursesSevices.GetOne(Courseid, DepID, SemesterID);
				_semesterCoursesSevices.Delete(s);
			}
			return RedirectToAction("Index", new { DepID = DepID });
		}
        public ActionResult ShowSemesterStu()
        {
            ViewBag.DepID = new SelectList(_DepartmentsServices.ReadAll(), "DepID", "DepName");

            int SemID = (int)Session["SemesterID"];
            IEnumerable<User> users = _usersServices.getSemesterUser(SemID);
          
            return View(users);
        }
        public ActionResult ShowSemesterCoursesForStu(int? StuID)
        {
            int SemID = (int)Session["SemesterID"];
            SemesterStudent semstud = _semesterStudentServices.getSemesterStudId(StuID.Value, SemID);
            IEnumerable<SemStudentCours> courses = _semStudentCoursesServices.GetResultForSemester(semstud.SemStuID);
            return PartialView("ShowSemesterCoursesForStu", courses);


        }
        public ActionResult SearchStu(int? DepIDSelected = 0)
        {
            if (DepIDSelected == 0)
            {
                ViewBag.DepID = new SelectList(_DepartmentsServices.ReadAll(), "DepID", "DepName");
                return PartialView("_ShowSemesterStu", _usersServices.ReadAllStudent().OrderBy(x => x.DepartmentID));
            }
            else
            {

                return PartialView("_ShowSemesterStu", _usersServices.GetForSearch(DepIDSelected.Value));

            }
        }

        public ActionResult EditMaxHours(int? StuID)
        {
            int SemID = (int)Session["SemesterID"];
            SemesterStudent semstud = _semesterStudentServices.getSemesterStudId(StuID.Value, SemID);
            return PartialView("_EditMaxHours", semstud);
        }
        [HttpPost]
        public ActionResult EditMaxHours(SemesterStudent semstud)
        {
            SemesterStudent semStud = _semesterStudentServices.getSemesterStudId(semstud.StudentID, semstud.SemesterID);
            semStud.MaxCreditHours = semstud.MaxCreditHours;
            _semesterStudentServices.updateCreditHouers(semStud);
            return Redirect("ShowSemesterStu");
        }
        public ActionResult OnSuccess()
        {
            return PartialView("_OnSuccess");
        }
        [HttpPost]
        public ActionResult OnSuccess(int? Done)
        {
            return Redirect("ShowSemesterStu");
        }

        public ActionResult OnFailure()
        {
            return PartialView("_OnFailure");
        }
        [HttpPost]
        public ActionResult OnFailure(int? Done)
        {
            return Redirect("ShowSemesterStu");
        }

    }
}      
