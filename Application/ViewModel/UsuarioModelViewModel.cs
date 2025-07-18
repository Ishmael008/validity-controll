
﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValidityControl.DoMain;
using ValidityControl.Infraestrutura;

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValidityControl.DoMain;
using ValidityControl.Infraestrutura;


namespace ValidityControl.Application.ViewModel
{
    public class UsuarioModelViewModel
    {
        [Required]
        public string? Name { get; set; }



        [Required]
        public string? Password { get; set; }


    }
}
