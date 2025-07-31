using ValidityControl.DoMain;

namespace ValidityControl.DoMain.Models
{
    public interface IProductControlRepository
    {

        Task Add(ProductControl productControl);

        List<ProductControl> GetProducts();
        Task<ProductControl> Update(ProductControl product, string ean);
        Task<ProductControl> GetForEan(string ean);
        Task<bool> Delete(string ean);
        bool ExistsbyGet(string ean);
        Task RemoveProduct();
    }
}
