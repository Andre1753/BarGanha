using BarGanha.BLL.Models;
using System.Threading.Tasks;

namespace BarGanha.DAL.Interfaces
{
    public interface IFuncaoRepositorio : IRepositorioGenerico<Funcao>
    {
        Task AdicionarFuncao(Funcao funcao);

        new Task Atualizar(Funcao funcao);
    }
}
