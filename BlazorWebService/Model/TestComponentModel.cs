using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorWebService.Model
{
    public class TestComponentModel
    {

        [JsonPropertyName("title")]
        public String Title { get; set; }
        [JsonPropertyName("content")]
        public String Content { get; set; }
    }
}
