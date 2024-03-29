﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BarGanha.BLL.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal Preco { get; set; }
        public bool Anunciar { get; set; }
        public bool Troca { get; set; }
        public string Descricao { get; set; }
        public string ImagemProduto { get; set; }

        public string UsuarioId { get; set; }
        public Usuario usuario { get; set; }

        public int CategoriaId { get; set; }
        public Categoria categoria { get; set; }
        public string Cidade { get; set; }
         public string Estado { get; set; }

        public ICollection<ProdutoOfertado> produtosOfertados { get; set; }
        public ICollection<ProdutoSelecionado> produtosSelecionados { get; set; }

    }
}