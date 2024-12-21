using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class BlogModel
    {
        public Blog Record { get; set; }

        public string Title => Record?.Title ?? string.Empty;

        public string Content => Record?.Content ?? string.Empty;

        public string Rating => Record?.Rating?.ToString() ?? string.Empty;

        public string PublishDate => Record?.PublishDate?.ToString("MM/dd/yyyy") ?? string.Empty;

        [DisplayName("Published By")]
        public string User => Record?.User?.UserName ?? string.Empty;

        public string Tags => string.Join("<br>", Record?.BlogTags?.Select(bt => bt.Tag?.Name) ?? Enumerable.Empty<string>());

        [DisplayName("Tags")]
        public List<int> TagIds
        {
            get => Record?.BlogTags?.Select(bt => bt.TagId).ToList() ?? new List<int>();
            set => Record.BlogTags = value?.Select(v => new BlogTag { TagId = v }).ToList() ?? new List<BlogTag>();
        }
    }
}

