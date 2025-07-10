using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        [JsonPropertyName("password")]
        public string password { get; private set; }
        

        public UsuarioModel() { }
        public UsuarioModel(string name,  string password)
        {

            this.name = name;
            this.password = password;
        }
    }
}
