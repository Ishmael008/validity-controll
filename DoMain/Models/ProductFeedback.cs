using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValidityControl.DoMain.Models
{
    [Table("ProductFeedback")]
    public class ProductFeedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int Id { get; set; }
       public string? Ean { get; set; }
       public string? Question1 { get; set; }
       public string? Question2 { get; set; }
       public DateTime? CreatedAt {  get; set; } 


        public ProductFeedback() { }

        public ProductFeedback(string Ean, string Question1, string Question2, DateTime CreatedAt) 
        {
            this.Ean = Ean;
            this.Question1 = Question1;
            this.Question2 = Question2;
            this.CreatedAt = CreatedAt;




        }


    }







}
