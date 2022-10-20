using System.Threading.Tasks;
using usuario.models;

namespace usuario.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> BuscaUsuarios();

        Task<Usuario> BuscaUsuarioPorId(int id);
        
        void AdiocionaUsuario(Usuario usuario);
        void AtualizaUsuario(Usuario usuario);
        void DeletaUsuario(Usuario usuario);

        Task<bool> SaveChangesAsync();
    }
}