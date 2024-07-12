using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

internal class UserFinanceSystem
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool Administrator { get; set; }
    public bool CurrentSystem { get; set; }

    [ForeignKey("FinanceSystem")]
    [Column(Order = 1)]
    public int IdSystem { get; set; }
    public virtual FinanceSystem FinanceSystem { get; set; }
}
