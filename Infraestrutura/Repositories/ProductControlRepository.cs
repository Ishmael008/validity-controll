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


        public ProductControlRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(ProductControl productControl)
        {
            _context.productControls.Add(productControl);
            await _context.SaveChangesAsync ();

        }
        public async Task<ProductControl> GetForEan(string ean)
        {
            return await _context.productControls.FirstOrDefaultAsync(x => x.ean == ean);
        }

        public List<ProductControl> GetProducts()
        {

            return _context.productControls.ToList();

        }


        public async Task<bool> Delete(string ean)
        {
            ProductControl productControl = await GetForEan(ean);

            _context.productControls.Remove(productControl);
           await _context.SaveChangesAsync();
            return true;
        }
        public bool ExistsbyGet(string ean)
        {

            return _context.productControls.Any(i =>  i.ean == ean);

           
        }

        public async Task RemoveProduct()
        {
            var today = DateTime.UtcNow.Date;
            var matury = _context.productControls
            .Where(v => v.Validity < today)
            .ToList();


            if (matury.Any())

            {
                 _context.productControls.RemoveRange(matury);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(string ean)
        {
           ProductControl product = await GetForEan(ean);
             _context.productControls.Update(product);
            await _context.SaveChangesAsync();
            
        }
    }
}
