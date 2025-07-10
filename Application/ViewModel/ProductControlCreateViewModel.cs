using System.Text.Json.Serialization;

namespace ValidityControl.Application.ViewModel
{
    public class ProductControlCreateViewModel
    {


        
            [JsonPropertyName("ean")]
            public string eanOfProduct { get; set; }

            [JsonPropertyName("name")]
            public string nameOfProduct { get; set; }

            [JsonPropertyName("validity")]
            public DateTime validity { get; set; }

            [JsonPropertyName("description")]
            public string description { get; set; }
        



    }
}
