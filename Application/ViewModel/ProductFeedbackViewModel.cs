using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace ValidityControl.Application.ViewModel
{
    public class ProductFeedbackViewModel
    {
        [Required]
        public string EanOfProduct { get; set; }
        [Required]
        public string QuestionOfProduct1 { get; set; }
        [Required]
        public string QuestionOfProduct2 { get; set; }
        [Required]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime CreatedAtProduct { get; set; }

    }
}
