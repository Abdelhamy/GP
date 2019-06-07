using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;

namespace CollageSystem.Data.Repository
{
	public interface ICoursesRepository : IRepository<Cours>
	{
		IEnumerable<Cours> GetCoursesForDepartment(int depid, int semesterID);
        IEnumerable<Cours> StudentCourseSelection(int semID, int studID);
        IEnumerable<Cours> GetStudentCoursesInSem(int semID, int studID);
    }
	public class CoursesRepository : RepositoryBase<Cours> , ICoursesRepository
	{
		public CoursesRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}

		public IEnumerable<Cours> GetCoursesForDepartment(int depid, int semesterID)
		{
			IEnumerable<Cours> Courses = (from ep in DbContext.Courses
										  join e in DbContext.CoursesSemesters on ep.CourseID equals e.CourseID
										  where e.DepartmentID == depid
										  && e.SemID == semesterID
										  select new { ep }).ToList().Select(x => new Cours
										  {
											  CourseID = x.ep.CourseID,
											  CourseCode = x.ep.CourseCode,
											  CourseName = x.ep.CourseName,
											  CoursePrerequsiteID = x.ep.CoursePrerequsiteID,
											  CTheoreticalHours = x.ep.CTheoreticalHours,
											  CPractiseHours = x.ep.CPractiseHours,
											  CCreditHours = x.ep.CCreditHours,
											  CourseRefID = x.ep.CourseRefID,
											  CourseRef = x.ep.CourseRef,
											  Cours1 = x.ep.Cours1
										
										  });

			return Courses;


		}

        public IEnumerable<Cours> GetStudentCoursesInSem(int semID, int studID)
        {
            IEnumerable<Cours> Courses = (from ep in DbContext.Courses
                                          join e in DbContext.SemStudentCourses on ep.CourseID equals e.CourseID
                                          join c in DbContext.SemesterStudents on e.SemStuID equals c.SemStuID

                                          where c.SemesterID == semID
                                          && c.StudentID == studID
                                          select new { ep }).ToList().Select(x => new Cours
                                          {
                                              CourseID = x.ep.CourseID,
                                              CourseCode = x.ep.CourseCode,
                                              CourseName = x.ep.CourseName,
                                              CoursePrerequsiteID = x.ep.CoursePrerequsiteID,
                                              CTheoreticalHours = x.ep.CTheoreticalHours,
                                              CPractiseHours = x.ep.CPractiseHours,
                                              CCreditHours = x.ep.CCreditHours,
                                              CourseRefID = x.ep.CourseRefID,
                                              CourseRef = x.ep.CourseRef,
                                              Cours1 = x.ep.Cours1

                                          });

            return Courses;
        }

        public IEnumerable<Cours> StudentCourseSelection(int semID, int studID)
        {

            List<Cours> send = new List<Cours>();

            List<Cours> studentCorces = new List<Cours>();
            int? DepID = DbContext.Users.Where(a => a.ID == studID).Select(a => a.DepartmentID).FirstOrDefault();
            if (DepID != null)
            {

                IEnumerable<Cours> currentSem = GetCoursesForDepartment(DepID.Value, semID);

                IEnumerable<SemStudentCours> semStudentCours = (from e in DbContext.SemStudentCourses
                                                                join s in DbContext.SemesterStudents on e.SemStuID equals s.SemStuID
                                                                where s.StudentID == studID
                                                                select e);

                foreach (var value in currentSem)
                {
                    if (semStudentCours.Count() != 0)
                    {

                        if (value.CoursePrerequsiteID != null)
                        {
                            foreach (var course in semStudentCours)
                            {
                                if ((value.CoursePrerequsiteID == course.CourseID && course.passed == true))
                                {
                                    studentCorces.Add(value);
                                }

                            }
                        }
                        else
                        {
                            studentCorces.Add(value);
                        }
                    }
                    else if (value.CoursePrerequsiteID == null)
                    {

                        studentCorces.Add(value);
                    }
                }

                int count = 0;
                if (studentCorces.Count != 0)
                {
                    foreach (var value in studentCorces)
                    {
                        foreach (var value2 in semStudentCours)
                        {
                            if (value.CourseID == value2.CourseID)
                            {
                                count += 1;
                            }
                        }
                        if (count == 0 || count > 1)
                        {

                            send.Add(value);

                        }
                        count = 0;

                    }
                }


            }



            return send;
        }

    }

}
