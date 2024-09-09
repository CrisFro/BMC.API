using BMC.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.UserName) ||
                string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest("Faltam dados. Favor entrar em contato com o suporte!");
            }

            var user = new ApplicationUser
            {
                UserName = login.UserName,
            };

            var result = await _userManager.CreateAsync(user, login.Password);

            if (result.Succeeded)
            {
                return Ok("Usuário adicionado!");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpGet("/api/ListaUsuarios")]
        public IActionResult ListUser()
        {
            var users = _userManager.Users.ToList();

            var simplifiedUserList = users.Select(user => new
            {
                UserId = user.Id,
                user.UserName,
            });

            return Ok(simplifiedUserList);
        }      

    }

}
