using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAPI.Models
{
    public class Author
    {
        public Author()
        {
            NewsList = new List<News?>();

        }
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }
        public virtual ICollection<News?> NewsList { get; set; }
        [ForeignKey(nameof(IdentityUser))]
        public string? SecurityID { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }

    }
}
