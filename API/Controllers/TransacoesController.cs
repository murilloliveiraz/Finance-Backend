using Domain.Interfaces.IService;
using Domain.Interfaces.ITransaction;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransaction _InterfaceTransacoes;
        private readonly ITransactionService _ITransacoesServico;
        public TransacoesController(ITransaction InterfaceTransacoes, ITransactionService ITransacoesServico)
        {
            _InterfaceTransacoes = InterfaceTransacoes;
            _ITransacoesServico = ITransacoesServico;
        }

        [HttpGet("/api/ListarDespesasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarDespesasUsuario(string emailUsuario)
        {
            return await _InterfaceTransacoes.GetUserTransactions(emailUsuario);
        }

        [HttpPost("/api/AdicionarDespesa")]
        [Produces("application/json")]
        public async Task<object> AdicionarDespesa(Transaction despesa)
        {
            await _ITransacoesServico.AdicionarTransacao(despesa);

            return despesa;

        }

        [HttpPut("/api/AtualizarDespesa")]
        [Produces("application/json")]
        public async Task<object> AtualizarDespesa(Transaction despesa)
        {
            await _ITransacoesServico.AtualizarTransacao(despesa);

            return despesa;

        }


        [HttpGet("/api/ObterDespesa")]
        [Produces("application/json")]
        public async Task<object> ObterDespesa(int id)
        {
            return await _InterfaceTransacoes.GetEntityById(id);
        }


        [HttpDelete("/api/DeleteDespesa")]
        [Produces("application/json")]
        public async Task<object> DeleteDespesa(int id)
        {
            try
            {
                var categoria = await _InterfaceTransacoes.GetEntityById(id);
                await _InterfaceTransacoes.Delete(categoria);

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpGet("/api/CarregaGraficos")]
        [Produces("application/json")]
        public async Task<object> CarregaGraficos(string emailUsuario)
        {
            return await _ITransacoesServico.CarregaGraficos(emailUsuario);
        }

    }
}
