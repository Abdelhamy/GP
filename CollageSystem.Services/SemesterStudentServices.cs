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
    public interface ISemesterStudentServices
    {
        void AddSemesterToStudents(int SemID, IEnumerable<User> users , Semester semester);
        SemesterStudent getSemesterStudId(int stu, int semID);
        int getMaxHouersForStudent(int semID, int stu);
        void updateCreditHouers(SemesterStudent semstud);
    }
    public class SemesterStudentServices : BaseService, ISemesterStudentServices
    {
        private readonly ISemesterStudentRepository _SemesterStudentRepository;

        public SemesterStudentServices(IUnitOfWork unitOfWork, ISemesterStudentRepository SemesterStudentRepository) : base(unitOfWork)
        {
            this._SemesterStudentRepository = SemesterStudentRepository;
        }

        public void AddSemesterToStudents(int SemID, IEnumerable<User> users , Semester semester)
        {
            //SemesterStudent semester = _SemesterStudentRepository.GetMany(x => x.Semester.SemID == SemID).FirstOrDefault();
            if (semester.SemNumber == 3)
            {
                foreach (var user in users)
                {
                    SemesterStudent semesterStudent = new SemesterStudent
                    {
                        SemesterID = SemID,
                        StudentID = user.ID,
                        MaxCreditHours = 9
                    };
                    _SemesterStudentRepository.Add(semesterStudent);

                }
            }
            else
            {
                foreach (var user in users)
                {
                    List<float> TotalGPAs = new List<float>(2);

                    SemesterStudent semesterStudent = new SemesterStudent
                    {
                        SemesterID = SemID,
                        StudentID = user.ID
                    };
                    List<SemesterStudent> semesterStudents = _SemesterStudentRepository.GetMany(x => x.StudentID == user.ID).ToList();
                    if (semesterStudents.Count() + 1 == 1)
                    {
                        semesterStudent.MaxCreditHours = 16;
                    }
                    else if (semesterStudents.Count() + 1 == 2)
                    {
                        TotalGPAs.Add((float)semesterStudents[0].SemesterGPA);
                        if (TotalGPAs[0] < 2)
                            semesterStudent.MaxCreditHours = 12;
                        else
                            semesterStudent.MaxCreditHours = 16;
                    }
                    else
                    {
                        float GPAHelper = 0;
                        int TotalHours = 0;
                        for (int i = 0; i < semesterStudents.Count(); i++)
                        {
                            TotalHours += semesterStudents[i].SemesterHours.Value;
                            GPAHelper += (float)(semesterStudents[i].SemesterGPA.Value * semesterStudents[i].SemesterHours.Value);

                            if (i == semesterStudents.Count() - 2)
                            {
                                TotalGPAs.Add(GPAHelper / TotalHours);
                            }
                            else if (i == semesterStudents.Count() - 1)
                            {
                                TotalGPAs.Add(GPAHelper / TotalHours);
                            }
                        }
                        if (TotalGPAs[1] < 2)
                        {
                            if (TotalGPAs[0] < 2)
                            {
                                semesterStudent.MaxCreditHours = 9;
                            }
                            else
                            {
                                semesterStudent.MaxCreditHours = 12;
                            }
                        }
                        else
                        {
                            semesterStudent.MaxCreditHours = 19;
                        }
                    }
                    _SemesterStudentRepository.Add(semesterStudent);
                }


            }
            unitOfWork.Commit();
        }

        public SemesterStudent getSemesterStudId(int stu, int semID)
        {
            return _SemesterStudentRepository.GetMany(x => x.SemesterID == semID && x.StudentID == stu).FirstOrDefault();
        }
        public void updateCreditHouers(SemesterStudent semstud)
        {
            unitOfWork.Commit();
        }
        public int getMaxHouersForStudent(int semID, int stu)
        {
            return _SemesterStudentRepository.GetMany(x => x.SemesterID == semID && x.StudentID == stu).Select(a => a.MaxCreditHours).FirstOrDefault();

        }
    }
}
