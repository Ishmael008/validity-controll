using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ValidityControl.DoMain.Models
{
    [Table("ProductControl")]
    public class ProductControl
    {

        public string? ean { get; set; }

        public string name {  get; set; }

        [Column("validity")]
        public DateTime Validity
        {
            get => _validity;
            set => _validity = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        private DateTime _validity;

        public string description {  get; set; }

        







        public ProductControl(string ean, string name, DateTime validity, string description)
        {
            this.ean = ean;
            this.name = name;
            this.Validity = validity;
            this.description = description;

        }

        public ProductControl() { }






    }
}
