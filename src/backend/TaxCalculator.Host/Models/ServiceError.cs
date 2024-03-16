using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace TaxCalculator.Host.Models
{
    [DataContract]
    public class ServiceError
    {
        [Required]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [Required]
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(Required = Required.Always)]
        public string Code { get; set; }

        [Required]
        [DataMember(Name = "message", EmitDefaultValue = false)]
        [JsonProperty(Required = Required.Always)]
        public string Message { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}