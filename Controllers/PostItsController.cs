using BMC.API.Models;
using BMC.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BMC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostItsController : ControllerBase
    {
        private readonly IPostItRepository _postItRepository;

        public PostItsController(IPostItRepository postItRepository)
        {
            _postItRepository = postItRepository;
        }

        // GET: api/PostIts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostIt>>> GetPostIts()
        {
            var postIts = await _postItRepository.GetAllPostItsAsync();
            return Ok(postIts);
        }

        [HttpGet("Canvas/{canvasId}")]
        public async Task<ActionResult<IEnumerable<PostIt>>> GetPostItsByCanvas(int canvasId)
        {
            var postIts = await _postItRepository.GetPostItsByCanvasAsync(canvasId);

            if (postIts == null)
            {
                return NotFound();
            }

            return Ok(postIts);
        }

        // GET: api/PostIts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostIt>> GetPostIt(int id)
        {
            var postIt = await _postItRepository.GetPostItByIdAsync(id);

            if (postIt == null)
            {
                return NotFound();
            }

            return Ok(postIt);
        }

        // GET: api/PostIts/Canvas/5/Block/
        [HttpGet("Canvas/{canvasId}/Block/{block}")]
        public async Task<ActionResult<IEnumerable<PostIt>>> GetPostItsByCanvasAndBlock(int canvasId, BMCBlock block)
        {
            var postIts = await _postItRepository.GetPostItsByCanvasAndBlockAsync(canvasId, block);
            return Ok(postIts);
        }

        // POST: api/PostIts
        [HttpPost]
        public async Task<ActionResult<PostIt>> PostPostIt(PostIt postIt)
        {
            await _postItRepository.AddPostItAsync(postIt);
            return CreatedAtAction(nameof(GetPostIt), new { id = postIt.Id }, postIt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostIt(int id, PostIt postIt)
        {
            if (id != postIt.Id)
            {
                return BadRequest();
            }

            var updatedPostIt = await _postItRepository.UpdatePostItAsync(postIt);
            return Ok(updatedPostIt);
        }

        // DELETE: api/PostIts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostIt(int id)
        {
            await _postItRepository.DeletePostItAsync(id);
            return NoContent();
        }


    }
}
