using ValidityControl.DoMain;
using ValidityControl.DoMain.Models;

namespace ValidityControl.Application.ViewModel
{
    public class ProductControlViewModel
    {
        public string eanOfProduct { get; set; }
        public string nameOfProduct { get; set; }
        public DateTime validity { get; set; }
        public Int32 daysToMatury { get; set; }
        public string description { get; set; }
        public ProductControlViewModel() { }
        public ProductControlViewModel(ProductControl productControl)
        {
            eanOfProduct = productControl.ean;
            nameOfProduct = productControl.name;
            validity = productControl.Validity;
            // DiasParaVencimento = productControl.daysToMatury;
            daysToMatury = (productControl.Validity.Date - DateTime.UtcNow.Date).Days;
            description = productControl.description;
        }


    }
}
