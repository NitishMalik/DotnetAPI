using DotnetAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetAPI.Test
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {

        }

        public void Seed(BlogDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Category.AddRange(
                 new Category() { Name="CSHARP", Slug ="csharp" },
                 new Category() { Name = "VISUAL STUDIO", Slug = "vsStudio" },
                 new Category() { Name = "ASP.NET CORE", Slug = "aspnetcore" },
                 new Category() { Name = "SQL SERVER", Slug = "sqlserver" }
                );

            context.Post.AddRange(
                new Post() { Title = "Test Title 1", Description = "Test Description 1", CategoryId = 2, CreatedDate = DateTime.Now },
                new Post() { Title = "Test Title 2", Description = "Test Description 2", CategoryId = 3, CreatedDate = DateTime.Now }
            );

            context.SaveChanges();
        }
    }
}
