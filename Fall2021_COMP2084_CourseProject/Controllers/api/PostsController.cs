using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fall2021_COMP2084_CourseProject.Data;
using Fall2021_COMP2084_CourseProject.Models;
/*
 =Methods for API controller=
 (On your browser, you can see GET() only. So, you should use Postman application to see all methods.)
 GET   : Read the data
 PUT   : Update the data
 POST  : Create the data
 DELETE: no return, no content

 *In the web controller, you have GET(when loading page) and POST(when submitting a form) methods only.
 */


namespace SearchInRoom.Controllers.api
{
    [Route("api/[controller]")]   
    [ApiController]//[ApiController] decorator: All of the response types will be JSON format by default
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts
                .OrderBy(p => p.PostedDate) //Make the JSON list sorted by the posted date
                .ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                //Return the JSON status (status: 404)
                return NotFound();
            }

            //All methods inside the API controller returns data in JSON format
            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                //Return the JSON status (status: 400)
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    //Return the JSON status (status: 404)
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //Return the JSON status (status: 204)
            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                //Return the JSON status (status: 404)
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            //Return the JSON status (status: 204)
            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
