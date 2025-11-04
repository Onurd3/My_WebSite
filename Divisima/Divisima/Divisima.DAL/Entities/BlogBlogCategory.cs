using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
	public class BlogBlogCategory
	{
        public int BlogID { get; set; }
        public Blog Blog { get; set; }

		public int BlogCategoryID { get; set; }
		public BlogCategory BlogCategory { get; set; }
	}
}
