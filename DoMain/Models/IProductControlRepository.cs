using ValidityControl.DoMain;

namespace ValidityControl.DoMain.Models
{
    public interface IProductControlRepository
    {

        void Add(ProductControl productControl);

        List<ProductControl> GetProductsToday();

        Task<ProductControl> GetForEan(string ean);
        Task<bool> Delete(string ean);
        bool ExistsbyGet(string ean);
        void RemoveProduct();
    }
}
