using CollageSystem.Core;
using CollageSystem.Data.Infrastructure;
using CollageSystem.Data.Repository;
using CollageSystem.Services.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageSystem.Services
{
    public interface ISemStudentCoursesServices
    {
        IEnumerable<SemStudentCours> GetResultForSemester(int semStuID);
        void AddNewCourse(SemStudentCours id);
        void DeleteCourse(SemStudentCours id);
        SemStudentCours getByid(int courseID, int semStuID);
        int UpdateMarks(SemStudentCours semStudentCours);
        //IEnumerable<SemStudentCours> getAllsemStudentCoursForStudent(int iD);
    }
    public class SemStudentCoursesServices : BaseService, ISemStudentCoursesServices
    {
        private readonly ISemStudentCoursesRepository _SemStudentCoursesRepository;

        public SemStudentCoursesServices(IUnitOfWork unitOfWork, ISemStudentCoursesRepository SemStudentCoursesRepository) : base(unitOfWork)
        {
            this._SemStudentCoursesRepository = SemStudentCoursesRepository;
        }

        public void AddNewCourse(SemStudentCours id)
        {
            _SemStudentCoursesRepository.Add(id);

            unitOfWork.Commit();

        }

        public void DeleteCourse(SemStudentCours id)
        {
            _SemStudentCoursesRepository.Delete(id);
            unitOfWork.Commit();
        }

        public SemStudentCours getByid(int courseID, int semStuID)
        {
            return _SemStudentCoursesRepository.GetMany(s => s.SemStuID == semStuID && s.CourseID == courseID).FirstOrDefault();
        }



        public IEnumerable<SemStudentCours> GetResultForSemester(int semStuID)
        {
            return _SemStudentCoursesRepository.GetMany(s => s.SemStuID == semStuID);
        }

        public int UpdateMarks(SemStudentCours semStudentCours)
        {
            var semStudentCours_ = _SemStudentCoursesRepository.GetMany(s => s.SemStuID == semStudentCours.SemStuID && s.CourseID == semStudentCours.CourseID).FirstOrDefault();

            semStudentCours_.FinalMarks = semStudentCours.FinalMarks;
            semStudentCours_.MidTermMarks = semStudentCours.MidTermMarks;
            semStudentCours_.OralMarks = semStudentCours.OralMarks;
            semStudentCours_.PractiseMarks = semStudentCours.PractiseMarks;
            semStudentCours_.YearWorkMarks = semStudentCours.YearWorkMarks;
            semStudentCours_.passed = semStudentCours.passed;

            _SemStudentCoursesRepository.Update(semStudentCours_);
            return unitOfWork.Commit();
        }
    }
}
