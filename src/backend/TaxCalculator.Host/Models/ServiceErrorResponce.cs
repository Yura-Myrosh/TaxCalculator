using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TaxCalculator.Host.Models
{
    public class ServiceErrorResponce
    {
        [Required]
        [DataMember(Name = "error", EmitDefaultValue = false)]
        [JsonProperty(Required = Required.Always)]
        public ServiceError Error { get; set; }
    }
}
