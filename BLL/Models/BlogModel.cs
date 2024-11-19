
using BLL.DAL;
using System.ComponentModel;


namespace BLL.Models
{
    public class BlogModel
    {
        public Blog Record { get; set; }

        public string Title => Record.Title;

        public string Content => Record.Content;

        public string Rating => Record.Rating.HasValue ? Record.Rating.Value.ToString() : string.Empty;

        public string PublishDate => Record.PublishDate.ToString("MM/dd/yyyy");

        [DisplayName("Published By")]
        public string User => Record.User.UserName;
    }
}
