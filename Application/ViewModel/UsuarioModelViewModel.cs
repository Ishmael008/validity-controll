using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidityControl.DoMain;
using ValidityControl.Infraestrutura;
namespace ValidityControl.Application.ViewModel
{
    public class UsuarioModelViewModel
    {
        
        public string? Name { get; set; }
        public string? Email {  get; set; }

    }
}
