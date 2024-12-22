using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="User name is required!")]
        [StringLength(20,ErrorMessage = "User Name must be maximum {1} characters!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter the {0}.")]
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}