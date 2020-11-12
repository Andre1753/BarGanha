using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Usuario : IdentityUser<string>
    {
        public string CPF { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }


    }

}
