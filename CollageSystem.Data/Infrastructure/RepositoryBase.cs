using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CollageSystem.Core;

namespace CollageSystem.Data.Infrastructure
{
	public abstract class RepositoryBase<T> where T : class
	{
		#region Properties

		private CollageSystemEntities dataContext;

		private IDbSet<T> dbSet;
		protected IDbFactory DbFactory
		{
			get;
			private set;
		}
		public CollageSystemEntities DbContext
		{
			get
			{
				return dataContext ?? (dataContext = DbFactory.Init());
			}
		}
		/// <summary>
		/// Entities
		/// </summary>
		protected virtual IDbSet<T> Entities
		{
			get
			{
				if (dbSet == null)
					dbSet = dataContext.Set<T>();
				return dbSet;
			}
		}

		/// <summary>
		/// Gets a table
		/// </summary>
		public virtual IQueryable<T> Table
		{
			get
			{
				return this.Entities;
			}
		}

		/// <summary>
		/// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
		/// </summary>
		public virtual IQueryable<T> TableNoTracking
		{
			get
			{
				return this.Entities.AsNoTracking();
			}
		}
		#endregion

		public RepositoryBase(IDbFactory dbFactory)
		{
			DbFactory = dbFactory;
			dbSet = DbContext.Set<T>();
		}


		#region Implementation

		public virtual T Add(T entity)
		{
			return dbSet.Add(entity);
		}
		public virtual void AddList(List<T> entities)
		{
			
			try
			{
				dataContext.Configuration.AutoDetectChangesEnabled = false;
				//dataContext.Configuration.ValidateOnSaveEnabled = false;

				foreach (var entity in entities)
				{
					dbSet.Add(entity);
				}

				//dataContext.SaveChanges();
			}
			finally
			{
				dataContext.Configuration.AutoDetectChangesEnabled = true;
				//dataContext.Configuration.ValidateOnSaveEnabled = true;

			}




		}
		public virtual void UpdateList(List<T> entities)
		{
			try
			{
				dataContext.Configuration.AutoDetectChangesEnabled = false;
				//dataContext.Configuration.ValidateOnSaveEnabled = false;
				
				foreach (var entity in entities)
				{
					dbSet.Attach(entity);
					dataContext.Entry(entity).State = EntityState.Modified;
					//dataContext.SaveChanges();
				}

				//dataContext.SaveChanges();
			}
			finally
			{
				dataContext.Configuration.AutoDetectChangesEnabled = true;
				//dataContext.Configuration.ValidateOnSaveEnabled = true;

			}
		}


		public virtual void Update(T entity)
		{

			dbSet.Attach(entity);
			dataContext.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public virtual void Delete(Expression<Func<T, bool>> where)
		{
			IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
			foreach (T obj in objects)
				dbSet.Remove(obj);
		}

		public virtual T GetById(int id)
		{
			return dbSet.Find(id);
		}

		public virtual T GetById(string id)
		{
			return dbSet.Find(id);
		}

		public virtual IEnumerable<T> GetAll()
		{
			return dbSet.ToList();
		}

		public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
		{
			return dbSet.Where(where).ToList();
		}
		public virtual IEnumerable<T> GetManyForPaging(Expression<Func<T, bool>> where , Expression<Func<T, int>> orderBy ,int pageNum , int pageSize)
		{
			return dbSet.Where(where).OrderBy(orderBy).Skip((pageNum -1) * pageSize).Take(pageSize);
		}

		//public virtual IEnumerable<TSource> DistinctBy<TSource, TKey>
		//    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		//{
		//    HashSet<TKey> seenKeys = new HashSet<TKey>();
		//    foreach (TSource element in source)
		//    {
		//        if (seenKeys.Add(keySelector(element)))
		//        {
		//            yield return element;
		//        }
		//    }
		//}

		public T Get(Expression<Func<T, bool>> where)
		{
			return dbSet.Where(where).FirstOrDefault<T>();
		}
		#endregion

	}
}
