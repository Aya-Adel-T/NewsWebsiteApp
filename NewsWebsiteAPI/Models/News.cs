using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsAPI.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey(nameof(Author))]
        public int AuthorID { get; set; }
        public virtual Author? Author { get; set; }
        public string OurNews { get; set; }
        public string? NewsImg { get; set; }
        [DataType(DataType.DateTime)]
        [publicationdate]
        public DateTime PublicationDate { get; set; }
        [DataType(DataType.DateTime)]

        public DateTime CreationDate { get; set; }
        [ForeignKey(nameof(IdentityUser))]
        public string? SecurityID { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }
    }
}
