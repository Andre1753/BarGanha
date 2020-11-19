using BarGanha.BLL.Models;
using System.Threading.Tasks;


namespace BarGanha.DAL.Interfaces
{
    public interface ICategoriaRepositorio : IRepositorioGenerico<Categoria>
    {
        Task<Categoria> PegarCategoriaPeloId(int CategoriaId);
    }
}
