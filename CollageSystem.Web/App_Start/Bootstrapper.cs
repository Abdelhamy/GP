
using Autofac;
using Autofac.Integration.Mvc;
using CollageSystem.Data.Infrastructure;
using CollageSystem.Data.Repository;
using CollageSystem.Services;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace CollageSystem.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutoFacContainer();
        }
        private static void SetAutoFacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            #region Repositories

            builder.RegisterAssemblyTypes(typeof(CourseRefRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(CoursesRepository).Assembly)
			   .Where(t => t.Name.EndsWith("Repository"))
			   .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(DepartmentsRepository).Assembly)
			   .Where(t => t.Name.EndsWith("Repository"))
			   .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(UsersRepository).Assembly)
			   .Where(t => t.Name.EndsWith("Repository"))
			   .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(SemestersRepository).Assembly)
		        .Where(t => t.Name.EndsWith("Repository"))
		        .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(SemNumbersRepository).Assembly)
	            .Where(t => t.Name.EndsWith("Repository"))
	             .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(SemesterCoursesRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(SemesterStudentRepository).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			#endregion

			#region Services

			builder.RegisterAssemblyTypes(typeof(CourseRefServices).Assembly)
                .Where(t => t.Name.EndsWith("Services"))
                .AsImplementedInterfaces().InstancePerRequest();

			builder.RegisterAssemblyTypes(typeof(CoursesService).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();

			builder.RegisterAssemblyTypes(typeof(DepartmentsServices).Assembly)
				.Where(t => t.Name.EndsWith("Services"))
				.AsImplementedInterfaces().InstancePerRequest();

			builder.RegisterAssemblyTypes(typeof(UsersServices).Assembly)
			.Where(t => t.Name.EndsWith("Services"))
			.AsImplementedInterfaces().InstancePerRequest();

			builder.RegisterAssemblyTypes(typeof(SemestersServices).Assembly)
			.Where(t => t.Name.EndsWith("Services"))
			.AsImplementedInterfaces().InstancePerRequest();

			builder.RegisterAssemblyTypes(typeof(SemNumbersServices).Assembly)
			.Where(t => t.Name.EndsWith("Services"))
			.AsImplementedInterfaces().InstancePerRequest();

			builder.RegisterAssemblyTypes(typeof(SemesterCoursesServices).Assembly)
			.Where(t => t.Name.EndsWith("Services"))
			.AsImplementedInterfaces().InstancePerRequest();
			builder.RegisterAssemblyTypes(typeof(SemesterStudentServices).Assembly)
			.Where(t => t.Name.EndsWith("Services"))
			.AsImplementedInterfaces().InstancePerRequest();

			#endregion


			IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}