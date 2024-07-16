using API.Models;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuario")]

        public async Task<IActionResult> AdicionaUsuario([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) ||
                string.IsNullOrWhiteSpace(login.senha) ||
                string.IsNullOrWhiteSpace(login.cpf))
            {
                return Ok("Falta alguns dados");
            }

            var user = new User
            {
                Email = login.email,
                UserName = login.email,
                CPF = login.cpf
            };

            var result = await _userManager.CreateAsync(user, login.senha);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            // Geração de confirmação caso precise 
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno do email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var Response = await _userManager.ConfirmEmailAsync(user, code);

            if (Response.Succeeded)
            {
                return Ok("Usuário Adicionado!");
            }
            else
            {
                return Ok("erro ao confirmar cadastro de usuário!");
            }

        }
    }
}
