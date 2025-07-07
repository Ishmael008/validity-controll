using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.Http.Headers;
using ValidityControl.Controllers;
using ValidityControl.DoMain;
using ValidityControl.DoMain.Models;

namespace ValidityControl.Infraestrutura.Repositories
{

    public class UsuarioRespository : IUsuarioRepository
    {
        private readonly ConnetionContext _connetion = new ConnetionContext();
        

        public void Add(UsuarioModel usuario)
        {

            _connetion.usuarios.Add(usuario);
            _connetion.SaveChanges();
        }


        public async Task<bool> Delete(int id)
        {
            UsuarioModel usuarioModel = GetForId(id);
            _connetion.Remove(usuarioModel);
            _connetion.SaveChanges();
            return true;
        }

        public List<UsuarioModel> Get()
        {

            return _connetion.usuarios.ToList();
               
            
        }
        public UsuarioModel GetByNameAndEmail(string name, string email)
        {
            return _connetion.usuarios.FirstOrDefault(u => u.name == name && u.email == email);
        }



        public UsuarioModel GetForId(int id)
        {
            
            return _connetion.usuarios.FirstOrDefault(x => x.id == id);
        }
    }
}
