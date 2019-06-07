using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CollageSystem.Core;
using CollageSystem.Services;
using System.Web.Mvc;

namespace CollageSystem.Web.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentSemestersController : Controller
    {

        ISemesterStudentServices _SemesterStudentServices;
        ISemStudentCoursesServices _SemStudentCoursesServices;
        ICoursesService _CoursesService;
        ISemestersServices _semestersServices;
        ICoursesService _coursesService;
        public StudentSemestersController(ISemesterStudentServices SemesterStudentServices, ISemStudentCoursesServices SemStudentCoursesServices, ICoursesService CoursesService, ISemestersServices semestersServices, ICoursesService coursesService)
        {
            _SemesterStudentServices = SemesterStudentServices;
            _SemStudentCoursesServices = SemStudentCoursesServices;
            _CoursesService = CoursesService;
            _semestersServices = semestersServices;
            _coursesService = coursesService;
        }


        // GET: Student/StudentSemesters
        public ActionResult Index()
        {
            User stu = (User)Session["User"];
            IEnumerable<Semester> semestersStudent = _semestersServices.GetSemestersForStudent(stu.ID);
            return View(semestersStudent);
        }
        public ActionResult SemesterResult(int semStuID)
        {
            return View(_SemStudentCoursesServices.GetResultForSemester(semStuID));
        }
        //pop
        public ActionResult AddCourses()
        {

            User stu = (User)Session["User"];
            int SemID = (int)Session["SemID"];
            IEnumerable<Cours> courses = _coursesService.StudentCourseSelection(SemID, stu.ID);
            return PartialView("_Create", courses);


        }

        public ActionResult AddOrDeleteCourses(int SemID)
        {
            User stu = (User)Session["User"];
            Session["SemID"] = SemID;
            IEnumerable<Cours> courses = _coursesService.StudentCourserInSem(SemID, stu.ID);
            int MaxHouers = _SemesterStudentServices.getMaxHouersForStudent(SemID, stu.ID);
            ViewBag.MaxHour = MaxHouers;
            if (courses != null)
            {
                int HouersCount = 0;
                foreach (var value in courses) {
                    HouersCount += value.CCreditHours;
                }
                ViewBag.allHouers = HouersCount;
            }
            else
            {
                ViewBag.allHouers = 0;
            }
            return View(courses);

        }

        public ActionResult AddCourseToStudent(int courseID)
        {
            User stu = (User)Session["User"];
            int SemID = (int)Session["SemID"];

            SemesterStudent semstud = _SemesterStudentServices.getSemesterStudId(stu.ID, SemID);
            Cours coures = _coursesService.GetById(courseID);
            int MaxHouers = _SemesterStudentServices.getMaxHouersForStudent(SemID, stu.ID);
            ViewBag.MaxHour = MaxHouers;

            IEnumerable<Cours> courses = _coursesService.StudentCourserInSem(SemID, stu.ID);

            int HouersCount = 0;
            if (courses != null)
            {
                
                foreach (var value in courses)
                {
                    HouersCount += value.CCreditHours;
                }
                ViewBag.allHouers = HouersCount;
            }
            else
            {
                ViewBag.allHouers = 0;
            }
            if (MaxHouers >= HouersCount + coures.CCreditHours) {

                if (semstud != null)
                {
                    //int.TryParse(semstudID.ToString(), out int semId);
                    SemStudentCours semStudentCours = new SemStudentCours()
                    {
                        CourseID = courseID,
                        SemStuID = semstud.SemStuID,
                        SemesterStudent = semstud,
                        Cours = coures

                    };
                    _SemStudentCoursesServices.AddNewCourse(semStudentCours);
                   courses = _coursesService.StudentCourserInSem(SemID, stu.ID);
                    ViewBag.allHouers = HouersCount + coures.CCreditHours;

                    semstud.SemesterHours = HouersCount + coures.CCreditHours;

                    _SemesterStudentServices.updateCreditHouers(semstud);
                }

            }


            return View("AddOrDeleteCourses", courses);
        }

        public ActionResult DeleteCourseFromStudent(int courseID)
        {
            User stu = (User)Session["User"];
            int SemID = (int)Session["SemID"];

            SemesterStudent semstud = _SemesterStudentServices.getSemesterStudId(stu.ID, SemID);
            Cours coures = _coursesService.GetById(courseID);
            SemStudentCours semStudentCours = _SemStudentCoursesServices.getByid(courseID, semstud.SemStuID);
            _SemStudentCoursesServices.DeleteCourse(semStudentCours);
            int MaxHouers = _SemesterStudentServices.getMaxHouersForStudent(SemID, stu.ID);
            ViewBag.MaxHour = MaxHouers;


            IEnumerable<Cours> courses = _coursesService.StudentCourserInSem(SemID, stu.ID);
            if (courses != null)
            {
                int HouersCount = 0;
                foreach (var value in courses)
                {
                    HouersCount += value.CCreditHours;
                }
                ViewBag.allHouers = HouersCount;

                semstud.SemesterHours = HouersCount;

                _SemesterStudentServices.updateCreditHouers(semstud);
            }
            else
            {
                ViewBag.allHouers = 0;
            }
            return View("AddOrDeleteCourses", courses);
        }

        public ActionResult DetailsOFTerm(int semId)
        {
            User stu = (User)Session["User"];
            Session["SemID"] = semId;
            SemesterStudent semstud = _SemesterStudentServices.getSemesterStudId(stu.ID, semId);
            IEnumerable<SemStudentCours> courses = _SemStudentCoursesServices.GetResultForSemester(semstud.SemStuID);


            return View(courses);
            
        }


    }
}