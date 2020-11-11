using Microsoft.AspNetCore.Identity;

namespace BarGanha.BLL.Models
{
    public class Funcao : IdentityRole<string>
    {
        public string Descricao { get; set; }
    }
}
