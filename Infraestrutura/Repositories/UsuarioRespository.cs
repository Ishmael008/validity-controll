using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly AppDbContext _connetion;

        public UsuarioRespository(AppDbContext connetion) 
        {
            _connetion = connetion;
        }

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
       



        
        public UsuarioModel GetByNameAndPassword(string name, string password)
        {
            return _connetion.usuarios.FirstOrDefault(u => u.name == name && u.password == password);

        }



        public UsuarioModel GetForId(int id)
        {

            return _connetion.usuarios.FirstOrDefault(x => x.id == id);
        }
    }
}
