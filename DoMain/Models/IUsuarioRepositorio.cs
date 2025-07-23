using ValidityControl.DoMain;

namespace ValidityControl.DoMain.Models
{
    public interface IUsuarioRepository
    {
         Task Add(UsuarioModel usuario);
        List<UsuarioModel> Get();
        UsuarioModel GetForId(int id);
        Task<bool> Delete(int id);


        Task<UsuarioModel> GetByNameAndPassword(string nome, string password);
     

    }
}
