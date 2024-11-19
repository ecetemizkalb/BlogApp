
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Tag
    {
        public int Id {  get; set; }

        [Required]
        public string Name { get; set; }

        public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
    }
}
