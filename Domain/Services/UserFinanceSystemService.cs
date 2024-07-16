using Domain.Interfaces.IUserFinanceSystem;
using Domain.Interfaces.ServiceInterfaces;
using Entities.Entities;

namespace Domain.Services
{
    public class UserFinanceSystemService : IUserFinanceSystemService
    {
        private readonly IUserFinanceSystem _userFinanceSystem;

        public UserFinanceSystemService(IUserFinanceSystem userFinanceSystem)
        {
            _userFinanceSystem = userFinanceSystem;
        }

        public async Task CadastrarUsuarioNoSistema(UserFinanceSystem usuario)
        {
            await _userFinanceSystem.Add(usuario);
        }
    }
}
