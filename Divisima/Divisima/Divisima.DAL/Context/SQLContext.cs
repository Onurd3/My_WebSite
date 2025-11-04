using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Divisima.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Divisima.DAL.Context
{
	public class SQLContext:DbContext
	{
        public SQLContext(DbContextOptions<SQLContext> opt): base(opt)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// DIAGRAM KODLARI BU DERSTE GELDİ
			modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);
			// add-migration category2
			///////////////////////////////
			///

			// DIAGRAM KODLARI BU DERSTE AMA ÜRÜNLER ENTITIYSİNDEN SONRA
			modelBuilder.Entity<ProductPicture>().HasOne(x => x.Product).WithMany(x => x.ProductPictures); // default olarak cascade
			modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);
			// add-migration product


			// DIAGRAM KODLARI BU DERSTE AMA BRAND TABLOSUNDAN SONRA
			modelBuilder.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);
			// add-migration Brand 


			//YENİ DERS 20
			modelBuilder.Entity<BlogBlogCategory>().HasKey(x => new { x.BlogID, x.BlogCategoryID });
			// YENİ END


			//data seed 
			modelBuilder.Entity<Admin>().HasData(new Admin
			{
				ID = 1,
				NameSurname = "Onur Ak",
				LastLoginDate=DateTime.Now,
				LastLoginIP =	"",
				UserName = "Onur",
				Password = "202cb962ac59075b964b07152d234b70"
			});
		}

		public DbSet<Slide> Slide { get; set; }
        public DbSet<Admin> Admin { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Product> Product  { get; set; }
		public DbSet<ProductPicture> ProductPicture { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderDetail> OrderDetail { get; set; }
		public DbSet<Blog> Blog { get; set; }
		public DbSet<BlogCategory> BlogCategory { get; set; }
		public DbSet<BlogBlogCategory> BlogBlogCategory { get; set; }



	}
}
