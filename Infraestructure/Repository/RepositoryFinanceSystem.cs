using Domain.Interfaces.IFinanceSystem;
using Entities.Entities;
using Infraestructure.Configurations;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infraestructure.Repository
{
    public class RepositoryFinanceSystem : RepositoryGeneric<FinanceSystem>, IFinanceSystem
    {
        private readonly DbContextOptions<ApplicationContext> _optionsBuilder;
        public RepositoryFinanceSystem()
        {
            _optionsBuilder = new DbContextOptions<ApplicationContext>();
        }
        public async Task<IEnumerable<FinanceSystem>> GetUserSystems(string userEmail)
        {
            using (var database = new ApplicationContext(_optionsBuilder))
            {
                return await
                    (from s in database.SistemaFinanceiro
                     join us in database.UsuarioSistemaFinanceiro on s.Id equals us.IdSystem
                     where us.Email.Equals(userEmail)
                     select s).AsNoTracking().ToListAsync();
            }
        }

        public async Task<bool> MakeACopyOfSystemTransactions()
        {
            var listSistemaFinanceiro = new List<FinanceSystem>();
            try
            {

                using (var banco = new ApplicationContext(_optionsBuilder))
                {
                    listSistemaFinanceiro = await banco.SistemaFinanceiro.Where(s => s.GenerateExpensesCopy).ToListAsync();
                }


                foreach (var item in listSistemaFinanceiro)
                {

                    using (var banco = new ApplicationContext(_optionsBuilder))
                    {

                        var dataatual = DateTime.Now;
                        var mes = dataatual.Month;
                        var ano = dataatual.Year;


                        var despesaJaExiste = await (from d in banco.Transacao
                                                     join c in banco.Categoria on d.IdCategory equals c.Id
                                                     where c.IdSystem == item.Id
                                                     && d.Month == mes
                                                     && d.Year == ano
                                                     select d.Id).AnyAsync();


                        if (!despesaJaExiste)
                        {

                            var despesasSistem = await (from d in banco.Transacao
                                                        join c in banco.Categoria on d.IdCategory equals c.Id
                                                        where c.IdSystem == item.Id
                                                        && d.Month == item.CopyMonth
                                                        && d.Year == item.CopyYear
                                                        select d).ToListAsync();

                            despesasSistem.ForEach(d =>
                            {
                                d.DueDate = new DateTime(ano, mes, d.DueDate.Day);
                                d.Month = mes;
                                d.Year = ano;
                                d.DateOfTheChange = DateTime.MinValue;
                                d.RegistrationDate = dataatual;
                                d.PaymentDate = DateTime.MinValue;
                                d.AlreadyPaid = false;
                                d.Id = 0;
                            });

                            if (despesasSistem.Any())
                            {
                                banco.Transacao.AddRange(despesasSistem);
                                await banco.SaveChangesAsync();
                            }


                        }

                    }

                }

            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

    }
}
