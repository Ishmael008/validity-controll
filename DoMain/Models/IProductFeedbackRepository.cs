namespace ValidityControl.DoMain.Models
{
    public interface IProductFeedbackRepository
    {

        Task Add(ProductFeedback feedback);
      
    }
}
