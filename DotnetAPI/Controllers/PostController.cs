using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetAPI.Models;
using DotnetAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostRepository postRepository;
        public PostController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        // GET: /<PostController>
        [HttpGet]
        public string GetWelcome()
        {
            return "Welcome to Dummy API";
        }

        // GET: /<PostController>
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await postRepository.GetCategories();
                if (categories == null)
                {
                    return NotFound();
                }
                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET /<PostController>/5
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await postRepository.GetPosts();
                if (posts == null)
                {
                    return NotFound();
                }
                return Ok(posts);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Route("GetPost")]
        public async Task<IActionResult> GetPost(int? postId)
        {
            if (postId == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await postRepository.GetPost(postId);
                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST /<PostController>
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromBody] Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await postRepository.AddPost(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // PUT /<PostController>/5
        [HttpPut]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost(int? id, [FromBody]Post model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != model.PostId)
            {
                return BadRequest();
            }
            
            try
            {
                await postRepository.UpdatePost(model);
                return Ok("Successfully update..");
            }
            catch (Exception)
            {
                return NotFound("Post not found");
            }                        
        }

        // DELETE /<PostController>/5
        [HttpDelete]
        [Route("DeletePost")]
        public async Task<IActionResult> Delete(int? postId)
        {
            int result = 0;
            if (postId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await postRepository.DeletePost(postId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
