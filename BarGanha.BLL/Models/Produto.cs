namespace BarGanha.BLL.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string ImagemProduto { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario usuario { get; set; }
    }
}