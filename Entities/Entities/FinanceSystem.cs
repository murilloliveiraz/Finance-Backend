using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

[Table("FinanceSystem")]
internal class FinanceSystem : Base
{
    public int Month { get; set; }
    public int Year { get; set; }
    public int Closingdate { get; set; }
    public bool GenerateExpensesCopy{ get; set; }
    public int CopyMonth { get; set; }
    public int CopyYear { get; set; }
}
