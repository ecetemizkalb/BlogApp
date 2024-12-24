
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class FavoritesModel
    {
        public int UserId { get; set; }

        public int BlogId { get; set; }

        [DisplayName("Blog Title")]
        public string BlogTitle { get; set; }   
    }
}
