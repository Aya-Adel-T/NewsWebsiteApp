using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NewsAPI.Models
{
    public class publicationdateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
    
            //DateTime publicationDate = (DateTime) value ;
        
            DateTime today = DateTime.Today;
            DateTime weekFromToday = today.AddDays(7);

            if ((DateTime)value >= today && (DateTime)value <= weekFromToday)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Publication date must be between today and a week from today.");
            }
        }
    }
}

