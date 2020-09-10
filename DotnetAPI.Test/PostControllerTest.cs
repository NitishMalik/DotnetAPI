using DotnetAPI.Controllers;
using DotnetAPI.Models;
using DotnetAPI.Repository;
using DotnetAPI.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DotnetAPI.Test
{
    public class PostControllerTest
    {
        private PostRepository repository;
        public static DbContextOptions<BlogDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=.\\;Database=blogdbTest;Trusted_Connection=True;";
        
        static PostControllerTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<BlogDbContext>().UseSqlServer(connectionString).Options;
        }

        public PostControllerTest()
        {
            var context = new BlogDbContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

            repository = new PostRepository(context);
        }

        #region GetByID
        [Fact]
        public async void Task_GetPostByID_Return_OkResult()
        {
            //Arrange
            var controller = new PostController(repository);
            var postId = 2;
            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetPostByID_Return_NotFoundResult()
        {
            //Arrange
            var controller = new PostController(repository);
            var postId = 2;
            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetPostByID_Return_BadRequestResult()
        {
            //Arrange
            var controller = new PostController(repository);
            var postId = 2;
            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetPostByID_Return_MatchResult()
        {
            //Arrange
            var controller = new PostController(repository);
            var postId = 2;
            //Act
            var data = await controller.GetPost(postId);

            //Assert
            Assert.IsType<NotFoundResult>(data);
            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var post = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

            Assert.Equal("Test Title 1", post.Title);
            Assert.Equal("Test Description 1", post.Description);
        }

        #endregion

    }
}
