
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using usuario.models;
using usuario.Repository;

namespace usuario.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsuarioController : ControllerBase   
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
           _repository = repository;
        }
       
        


        [HttpGet]
        public async Task<IActionResult> Get()
        {

           var usuarios = await _repository.BuscaUsuarios();
            return usuarios.Any()? Ok(usuarios)
            :  NoContent ();
        } 

         [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {

           var usuario = await _repository.BuscaUsuarioPorId(id);
            return usuario != null? Ok(usuario)
            :  NotFound("Usuário não encontrado");
        }
       

           
        [HttpPost]
        public async Task<IActionResult> Post(Usuario  usuario)
        {
            _repository.AdiocionaUsuario(usuario);
            return await _repository.SaveChangesAsync() ? Ok("Usuario cadastrado com sucesso") : BadRequest("Erro ao cadastrar usuario");
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
           var usuarioBanco = await _repository.BuscaUsuarioPorId(id);
           if(usuarioBanco == null) return NotFound("Usuario não encontrado");

           usuarioBanco.Nome = usuario.Nome?? usuarioBanco.Nome;
           usuarioBanco.DataNascimento = usuario.DataNascimento != new DateTime()?usuario.DataNascimento:usuarioBanco.DataNascimento;


           _repository.AtualizaUsuario(usuarioBanco);

           return await _repository.SaveChangesAsync() ? Ok("Usuario Atualizado"): BadRequest("Erro ao atualizar");
        }


        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioBanco = await _repository.BuscaUsuarioPorId(id);
            if(usuarioBanco == null) return NotFound("Usuario não encontrado");


            _repository.DeletaUsuario(usuarioBanco);

            return await _repository.SaveChangesAsync()? Ok("Usuario deletado com sucesso") : BadRequest("Erro ao Deletar usuario.");

        }
             
        



    }
}