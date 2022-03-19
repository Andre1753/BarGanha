﻿using System.ComponentModel.DataAnnotations;

namespace BarGanha.BLL.Models
{
    public class Oferta
    {
        public int OfertaId { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int ProdutoOfertadoId { get; set; }
        public Produto ProdutoOfertado { get; set; }
        public bool Aprovado { get; set; }
        public int Grupo { get; set; }
    }
}