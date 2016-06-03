using System.ComponentModel.DataAnnotations;

namespace EnrollmentApplication.Infrastructure.Core.HttpRequest
{
    public class RegistrationRequest
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required, MaxLength(100)]
        public string Job { get; set; }
        [Required, MaxLength(100)]
        public string JobPosition { get; set; }

        [MaxLength(100), RegularExpression(@"[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$")]
        public string Email { get; set; }
        [Required, MaxLength(25)]
        public string Phone { get; set; }
        [Required, MaxLength(500)]
        public string Address { get; set; }
    }
}
