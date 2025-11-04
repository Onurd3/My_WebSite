using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.BL.Repositories
{
	public interface IRepository<T> // IRepository<Admin> IRepository<Slide>
	{
		public IQueryable<T> GetAll();
		public IQueryable<T> GetAll(Expression<Func<T,bool>> expression); // linq sorgusu atılabilir halde bir yapı -- sorguya göre liste
		public T GetBy(Expression<Func<T, bool>> expression); // tek bir kayıt

		public void Add(T entity);

		// Bu Ders Gelen
		public void Update(T entity);


		public void Delete(T entity);
	}
}
