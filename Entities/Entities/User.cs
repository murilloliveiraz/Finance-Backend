using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

public class User : IdentityUser
{
    [Column("USER_CPF")]
    public string CPF { get; set; }
}
