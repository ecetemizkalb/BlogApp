using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services
{
    public class BlogService : Service, IService<Blog, BlogModel>
    {
        public BlogService(Db db) : base(db)
        {
        }

        public Service Create(Blog entity)
        {
            entity.Title = entity.Title?.Trim();
            entity.Content = entity.Content?.Trim();
            entity.PublishDate = DateTime.Now;
            _db.Blogs.Add(entity);
            _db.SaveChanges();
            return Success(entity.Title + " is created succesfully.");
        }

        public Service Delete(int id)
        {
            var e = _db.Blogs.SingleOrDefault(b => b.Id == id);
            if (e == null)
                Error("Blog to be deleted can not be found!");
            //control manytomany relationships with tag
            _db.Blogs.Remove(e);
            _db.SaveChanges();
            return Success("Blog is deleted.");
        }

        public IQueryable<BlogModel> Query()
        {
            return _db.Blogs.OrderBy(b => b.Title).Select(b => new BlogModel() { Record = b });
        }

        public Service Update(Blog entity)
        {
            // Fetch the existing blog from the database
            var e = _db.Blogs.SingleOrDefault(b => b.Id == entity.Id);
            if (e == null)
                return Error("Blog to be updated cannot be found!");

            e.Title = entity.Title?.Trim();
            e.Content = entity.Content?.Trim();
            e.Rating = entity.Rating;

            // Manytomany changes
            //_db.Blogs.Update(e);

            // Save changes to the database
            _db.SaveChanges();
            return Success("Blog has been updated successfully.");
        }
    }
}
