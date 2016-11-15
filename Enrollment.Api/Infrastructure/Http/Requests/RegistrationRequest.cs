using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Enrollment.Api.Infrastructure.Http.Requests
{
    public class RegistrationRequest
    {
        [Required]
        [MinLength(4)]
        [MaxLength(25)]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        [RegularExpression(@".*(\d[A-Za-z]|[A-Za-z]\d)+.*")]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
