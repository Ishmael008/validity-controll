using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValidityControl.DoMain;
using ValidityControl.Infraestrutura;

namespace ValidityControl.Application.ViewModel
{
    public class UsuarioModelViewModel
    {

        public string? Name { get; set; }
        public string? Password { get; set; }

    }
}
