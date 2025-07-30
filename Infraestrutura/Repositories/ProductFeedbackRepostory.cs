using Microsoft.AspNetCore.Http.HttpResults;
using ValidityControl.DoMain.Models;

namespace ValidityControl.Infraestrutura.Repositories
{
    public class ProductFeedbackRepostory: IProductFeedbackRepository
    {
        private readonly AppDbContext _AppDbContext;

        public ProductFeedbackRepostory(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public async Task Add(ProductFeedback feedback)
        {
            _AppDbContext.productFeedbacks.Add(feedback);
            await _AppDbContext.SaveChangesAsync();
             
          
        }

    }
}
