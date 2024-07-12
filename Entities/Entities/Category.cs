using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

internal class Category : Base
{
    [ForeignKey("FinanceSystem")]
    [Column(Order = 1)]
    public int IdSystem { get; set; }
    public virtual FinanceSystem FinanceSystem { get; set; }
}
