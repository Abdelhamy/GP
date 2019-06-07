using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Data.Repository
{
	public interface ISemestersRepository : IRepository<Semester>
	{
		IEnumerable<Semester> GetSemestersForStudent(int iD);
	}
	public class SemestersRepository : RepositoryBase<Semester>, ISemestersRepository
	{
		public SemestersRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}

		public IEnumerable<Semester> GetSemestersForStudent(int iD)
		{
			IEnumerable<Semester> SemestersStudent = (from ep in DbContext.Semesters
										  join e in DbContext.SemesterStudents on ep.SemID equals e.SemesterID
										  where e.StudentID == iD

										  select new { ep }).ToList().Select(x => new Semester
										  {
											  Year = x.ep.Year,
											  SemNumber = x.ep.SemNumber,
											  SemNumber1 = x.ep.SemNumber1,
											  Editable = x.ep.Editable,
                                              Added = x.ep.Added,
											  CoursesSemesters = x.ep.CoursesSemesters,
											  SemID = x.ep.SemID,
											  SemesterStudents = x.ep.SemesterStudents
										  });

			return SemestersStudent;
		}
	}
}
