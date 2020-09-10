﻿using DotnetAPI.Models;
using DotnetAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetAPI.Repository
{
    public interface IPostRepository
    {
        Task<List<Category>> GetCategories();
        Task<List<PostViewModel>> GetPosts();
        Task<PostViewModel> GetPost(int? postId);
        Task<int> AddPost(Post post);
        Task<int> DeletePost(int? postId);
        Task UpdatePost(Post post);
    }
}
