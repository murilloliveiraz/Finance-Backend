using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

internal class User : IdentityUser
{
    [Column("USER_CPF")]
    public string CPF { get; set; }
}
