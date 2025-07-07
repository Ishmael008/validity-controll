using ValidityControl.DoMain;

namespace ValidityControl.DoMain.Models
{
    public interface IUsuarioRepository
    {
        void Add(UsuarioModel usuario);
        List<UsuarioModel> Get();
        UsuarioModel GetForId(int id);
        Task<bool> Delete(int id);
        UsuarioModel GetByNameAndEmail(string name, string email);
    }
}
