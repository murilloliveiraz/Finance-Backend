using System.ComponentModel.DataAnnotations;

namespace Entities.Entities;

internal class Base : Notification
{
    [Display(Name = "Código")]
    public int Id { get; set; }
    [Display(Name = "Nome")]
    public string Name { get; set; }
}
