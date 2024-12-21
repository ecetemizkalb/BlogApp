
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BLL.DAL
{
    public class Blog
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public decimal? Rating {  get; set; }
        
        public DateTime? PublishDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } 

        public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
    }
}
