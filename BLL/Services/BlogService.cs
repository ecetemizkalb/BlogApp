using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class BlogService : Service, IService<Blog, BlogModel>
    {
        public BlogService(Db db) : base(db)
        {
        }

        public Service Create(Blog entity)
        {
            if (entity == null)
                return Error("Blog entity cannot be null.");

            if (string.IsNullOrWhiteSpace(entity.Title))
                return Error("Blog title cannot be empty.");

            if (_db.Blogs.Any(b => b.Title.ToLower() == entity.Title.ToLower().Trim()))
                return Error("Blog with the same title exists!");

            entity.Title = entity.Title?.Trim();
            entity.Content = entity.Content?.Trim();
            entity.PublishDate = entity.PublishDate ?? DateTime.Now;
            _db.Blogs.Add(entity);
            _db.SaveChanges();
            return Success(entity.Title + " is created successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Blogs.Include(b => b.BlogTags).SingleOrDefault(b => b.Id == id);
            if (entity is null)
                return Error("Blog can't be found!");

            if (entity.BlogTags != null)
                _db.BlogTags.RemoveRange(entity.BlogTags);
            _db.Blogs.Remove(entity);
            _db.SaveChanges();
            return Success("Blog deleted successfully.");
        }

        public IQueryable<BlogModel> Query()
        {
            return _db.Blogs
                    .Include(b => b.User)
                    .Include(b => b.BlogTags).ThenInclude(bt => bt.Tag)
                    .OrderByDescending(b => b.PublishDate)
                    .ThenByDescending(b => b.Rating)
                    .ThenBy(b => b.Title)
                    .Select(b => new BlogModel { Record = b });
        }

        public Service Update(Blog record)
        {
            if (record == null)
                return Error("Blog record cannot be null.");

            if (string.IsNullOrWhiteSpace(record.Title))
                return Error("Blog title cannot be empty.");

            if (_db.Blogs.Any(b => b.Id != record.Id && b.Title.ToLower() == record.Title.ToLower().Trim()))
                return Error("Another blog with the same title exists!");

            var entity = _db.Blogs.Include(b => b.BlogTags).SingleOrDefault(b => b.Id == record.Id);
            if (entity is null)
                return Error("Blog not found!");

            if (entity.BlogTags != null)
                _db.BlogTags.RemoveRange(entity.BlogTags);
            entity.Title = record.Title?.Trim();
            entity.Content = record.Content?.Trim();
            entity.Rating = record.Rating;
            entity.PublishDate = DateTime.Now;
            entity.UserId = record.UserId;
            entity.BlogTags = record.BlogTags ?? new List<BlogTag>();
            _db.Blogs.Update(entity);
            _db.SaveChanges();

            return Success("Blog has been updated successfully.");
        }
    }
}

