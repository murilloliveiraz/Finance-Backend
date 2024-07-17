using Domain.Interfaces.IFinanceSystem;
using Domain.Interfaces.IUserFinanceSystem;
using Domain.Interfaces.ServiceInterfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioSistemaFinanceiroController : ControllerBase
    {
        private readonly IUserFinanceSystem _InterfaceUsuarioSistemaFinanceiro;
        private readonly IUserFinanceSystemService _IUsuarioSistemaFinanceiroServico;
        public UsuarioSistemaFinanceiroController(IUserFinanceSystem InterfaceUsuarioSistemaFinanceiro,
            IUserFinanceSystemService IUsuarioSistemaFinanceiroServico)
        {
            _InterfaceUsuarioSistemaFinanceiro = InterfaceUsuarioSistemaFinanceiro;
            _IUsuarioSistemaFinanceiroServico = IUsuarioSistemaFinanceiroServico;
        }

        [HttpGet("/api/ListarUsuariosSistema")]
        [Produces("application/json")]
        public async Task<object> ListaSistemasUsuario(int idSistema)
        {
            return await _InterfaceUsuarioSistemaFinanceiro.GetSystemUsers(idSistema);
        }


        [HttpPost("/api/CadastrarUsuarioNoSistema")]
        [Produces("application/json")]
        public async Task<object> CadastrarUsuarioNoSistema(int idSistema, string emailUsuario)
        {
            try
            {
                await _IUsuarioSistemaFinanceiroServico.CadastrarUsuarioNoSistema(
                   new UserFinanceSystem
                   {
                       IdSystem = idSistema,
                       Email = emailUsuario,
                       Administrator = false,
                       CurrentSystem = true
                   });
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);

        }

        [HttpDelete("/api/DeleteUsuarioSistemaFinanceiro")]
        [Produces("application/json")]
        public async Task<object> DeleteUsuarioSistemaFinanceiro(int id)
        {
            try
            {
                var usuarioSistemaFinanceiro = await _InterfaceUsuarioSistemaFinanceiro.GetEntityById(id);

                await _InterfaceUsuarioSistemaFinanceiro.Delete(usuarioSistemaFinanceiro);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);

        }
    }
}
