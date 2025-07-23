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

            public async Task  Add(UsuarioModel usuario)
        {

            _connetion.usuarios.Add(usuario);
           await _connetion.SaveChangesAsync();
        }


        public async Task<bool> Delete(int id)
        {
            UsuarioModel usuarioModel = GetForId(id);
            _connetion.Remove(usuarioModel);
            await _connetion.SaveChangesAsync();
            return true;
        }

        public List<UsuarioModel> Get()
        {

            return _connetion.usuarios.ToList();

               
            
        }
       



        
        public async Task<UsuarioModel> GetByNameAndPassword(string name, string password)
        {
            return  await _connetion.usuarios.FirstOrDefaultAsync(u => u.name == name && u.password == password);

        }



        public UsuarioModel GetForId(int id)
        {

            return _connetion.usuarios.FirstOrDefault(x => x.id == id);
        }
    }
}
