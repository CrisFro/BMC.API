using BMC.API.Models;
using BMC.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BMC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessModelCanvasController : ControllerBase
    {
        private readonly IBusinessModelCanvasRepository _businessModelCanvasRepository;

        public BusinessModelCanvasController(IBusinessModelCanvasRepository businessModelCanvasRepository)
        {
            _businessModelCanvasRepository = businessModelCanvasRepository ?? throw new ArgumentNullException(nameof(businessModelCanvasRepository));
        }

        // GET: api/BusinessModelCanvas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusinessModelCanvas>>> GetBusinessModelCanvases()
        {
            var businessModelCanvases = await _businessModelCanvasRepository.GetAllBusinessModelCanvases();
            return Ok(businessModelCanvases);
        }

        // GET: api/BusinessModelCanvas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessModelCanvas>> GetBusinessModelCanvas(int id)
        {
            var businessModelCanvas = await _businessModelCanvasRepository.GetBusinessModelCanvasById(id);

            if (businessModelCanvas == null)
            {
                return NotFound();
            }

            return Ok(businessModelCanvas);
        }
        // GET: api/BusinessModelCanvas/Users/{userId} PEGA O CANVASID ASSOCIADO
        [HttpGet("Users/{userId}")]
        public async Task<ActionResult<BusinessModelCanvas>> GetCanvasByUserId(string userId)
        {
            var canvas = await _businessModelCanvasRepository.GetBusinessModelCanvasByUserId(userId);
            if (canvas == null)
            {
                return NotFound();
            }

            return Ok(canvas);
        }

        // GET: api/BusinessModelCanvas/5/Users/UserId PEGA O USERID ASSOCIADO
        [HttpGet("{canvasId}/UserId")]
        public async Task<ActionResult<string>> GetUserIdByCanvasId(int canvasId)
        {
            var canvas = await _businessModelCanvasRepository.GetBusinessModelCanvasById(canvasId);
            if (canvas == null)
            {
                return NotFound();
            }

            // Aqui você pode acessar o primeiro UserId associado ao BusinessModelCanvas com o canvasId
            var userId = canvas.UserCanvases.FirstOrDefault()?.UserId;

            if (userId == null)
            {
                return NotFound();
            }

            // Retornar o UserId como string
            return Ok(userId);
        }



        // POST: api/BusinessModelCanvas
        [HttpPost]
        public async Task<ActionResult<BusinessModelCanvas>> PostBusinessModelCanvas(BusinessModelCanvas businessModelCanvas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Salvando o BusinessModelCanvas sem associar um usuário inicialmente
                await _businessModelCanvasRepository.AddBusinessModelCanvas(businessModelCanvas);
                return CreatedAtAction(nameof(GetBusinessModelCanvas), new { id = businessModelCanvas.Id }, businessModelCanvas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/BusinessModelCanvas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessModelCanvas(int id, BusinessModelCanvas businessModelCanvas)
        {
            if (id != businessModelCanvas.Id)
            {
                return BadRequest();
            }

            try
            {
                await _businessModelCanvasRepository.UpdateBusinessModelCanvas(businessModelCanvas);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/BusinessModelCanvas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessModelCanvas(int id)
        {
            try
            {
                await _businessModelCanvasRepository.DeleteBusinessModelCanvas(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/BusinessModelCanvas/5/Users/{userId}
        [HttpPost("{canvasId}/Users/{userId}")]
        public async Task<IActionResult> AddUserToBusinessModelCanvas(int canvasId, string userId)
        {
            try
            {
                await _businessModelCanvasRepository.AddUserToBusinessModelCanvas(canvasId, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/BusinessModelCanvas/5/Users/{userId}
        [HttpDelete("{canvasId}/Users/{userId}")]
        public async Task<IActionResult> RemoveUserFromBusinessModelCanvas(int canvasId, string userId)
        {
            try
            {
                await _businessModelCanvasRepository.RemoveUserFromBusinessModelCanvas(canvasId, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}


