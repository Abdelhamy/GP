using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CollageSystem.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
		void AddList(List<T> entities);
		void UpdateList(List<T> entities);

		void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
		IEnumerable<T> GetManyForPaging(Expression<Func<T, bool>> where, Expression<Func<T, int>> orderBy, int pageNum, int pageSize);

		//IEnumerable<TSource> DistinctBy<TSource, TKey>
		//    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector);
		/// <summary>
		/// Gets a table
		/// </summary>
		IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}
