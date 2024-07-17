using Domain.Interfaces.ICategory;
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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategory _InterfaceCategoria;
        private readonly ICategoryService _ICategoriaServico;

        public CategoriaController(ICategory InterfaceCategoria, ICategoryService ICategoriaServico)
        {
            _InterfaceCategoria = InterfaceCategoria;
            _ICategoriaServico = ICategoriaServico;
        }

        [HttpGet("/api/ListarCategoriasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarCategoriasUsuario(string emailUsuario)
        {
            return await _InterfaceCategoria.GetUserCategories(emailUsuario);
        }

        [HttpPost("/api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Category categoria)
        {
            await _ICategoriaServico.AdicionarCategoria(categoria);

            return categoria;
        }

        [HttpPut("/api/AtualizarCategoria")]
        [Produces("application/json")]
        public async Task<object> AtualizarCategoria(Category categoria)
        {
            await _ICategoriaServico.AtualizarCategoria(categoria);

            return categoria;
        }

        [HttpGet("/api/ObterCategoria")]
        [Produces("application/json")]
        public async Task<object> ObterCategoria(int id)
        {
            return await _InterfaceCategoria.GetEntityById(id);
        }


        [HttpDelete("/api/DeleteCategoria")]
        [Produces("application/json")]
        public async Task<object> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _InterfaceCategoria.GetEntityById(id);
                await _InterfaceCategoria.Delete(categoria);

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
