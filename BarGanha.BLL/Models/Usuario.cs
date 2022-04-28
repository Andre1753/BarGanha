﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Usuario : IdentityUser<string>
    {
        public string CPF { get; set; }
        public string NomeCompleto { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public ICollection<Oferta> Ofertas { get; set; }
    }

}
