using System.ComponentModel.DataAnnotations;

 namespace NewsAPI.Models
{
    public class TokenRequestModel
    {
        [Required]
        [DataType(DataType.EmailAddress)

]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]


        public string Password { get; set; }
    }
}
