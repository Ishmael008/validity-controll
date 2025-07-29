namespace ValidityControl.DoMain.Models
{
    public interface IProductFeedbackRepository
    {

        Task AddFeedbackAsync(ProductFeedback feedback);
      
    }
}
