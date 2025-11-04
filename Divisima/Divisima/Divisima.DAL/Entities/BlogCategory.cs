using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Divisima.DAL.Entities
{
	public class BlogCategory
	{
		public int ID { get; set; }

		[Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Blog Kategori Adı")]
		public string Name { get; set; }

		[Display(Name="Görüntüleme Sırası")]
        public int DisplayIndex { get; set; }

		public ICollection<BlogBlogCategory> BlogBlogCategories { get; set; }
    }
}
