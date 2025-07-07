using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ValidityControl.DoMain.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; private set; }
        [JsonPropertyName("nome")]
        public string? name { get; private set; }
        [JsonPropertyName("email")]
        public string? email { get; private set; }

        public UsuarioModel() { }
        public UsuarioModel( string name, string email)
        {
            
            this.name = name;
            this.email = email;
        }
    }
}
