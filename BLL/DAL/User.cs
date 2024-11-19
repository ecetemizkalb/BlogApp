using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}