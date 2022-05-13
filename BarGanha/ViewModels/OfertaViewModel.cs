using BarGanha.BLL.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BarGanha.ViewModels
{
    public class OfertaViewModel
    {
        public IList<Ofertar> Ofertas { get; set; }
        public IList<Selecionar> Selecionados { get; set; }


        public string usuarioId { get; set; }
    }

    public class Selecionar
    {
        public int Id { get; set; }
        public bool Selected { get; set; }
    }

    public class Ofertar
    {
        public int Id { get; set; }
        public bool Selected { get; set; }
    }
}
