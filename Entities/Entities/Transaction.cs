using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

[Table("Transacoes")]
public class Transaction : Base
{
    public decimal Value { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public EnumTransactionType TransactionType { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime DateOfTheChange { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool AlreadyPaid { get; set; }
    public bool Overdue { get; set; }

    [ForeignKey("Category")]
    [Column(Order = 1)]
    public int IdCategory { get; set; }
    //public virtual Category Category { get; set; }

}
