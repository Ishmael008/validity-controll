using Microsoft.EntityFrameworkCore;
using ValidityControl.DoMain.Models;
using ValidityControl.Controllers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ValidityControl.DoMain;
using System.Linq.Dynamic.Core;





namespace ValidityControl.Infraestrutura.Repositories
{
    public class ProductControlRepository : IProductControlRepository
    {

       
        
        private readonly AppDbContext _context;




        public void Add(ProductControl productControl)
        {
            _context.productControls.Add(productControl);
            _context.SaveChanges();

        }
        public async Task<ProductControl> GetForEan(string ean)
        {
            return await _context.productControls.FirstOrDefaultAsync(x => x.ean == ean);
        }

        public List<ProductControl> GetProductsToday()
        {

            return _context.productControls.ToList();

        }


        public async Task<bool> Delete(string ean)
        {
            ProductControl productControl = await GetForEan(ean);

            _context.productControls.Remove(productControl);
            _context.SaveChanges();
            return true;
        }
        public bool ExistsbyGet(string ean)
        {

            return _context.productControls.Any(i =>  i.ean == ean);

           
        }

        public void RemoveProduct()
        {
            var today = DateTime.UtcNow.Date;
            var matury = _context.productControls
            .Where(v => v.Validity < today)
            .ToList();


            if (matury.Any())

            {
                _context.productControls.RemoveRange(matury);
                _context.SaveChanges();
            }
        }
    }
}
