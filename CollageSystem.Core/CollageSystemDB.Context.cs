﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CollageSystem.Core
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CollageSystemEntities : DbContext
    {
        public CollageSystemEntities()
            : base("name=CollageSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CourseRef> CourseRefs { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<CoursesSemester> CoursesSemesters { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<SemesterStudent> SemesterStudents { get; set; }
        public virtual DbSet<SemNumber> SemNumbers { get; set; }
        public virtual DbSet<SemStudentCours> SemStudentCourses { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}