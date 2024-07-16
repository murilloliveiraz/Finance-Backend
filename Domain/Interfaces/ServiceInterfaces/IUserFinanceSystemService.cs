using Entities.Entities;

namespace Domain.Interfaces.ServiceInterfaces
{
    public interface IUserFinanceSystemService
    {
        Task CadastrarUsuarioNoSistema(UserFinanceSystem usuario);
    }
}
