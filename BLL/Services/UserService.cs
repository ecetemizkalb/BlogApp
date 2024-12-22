using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService : Service, IService<User, UserModel>
    {
        public UserService(Db db) : base(db)
        {
        }

        public Service Create(User entity)
        {
            if (_db.Users.Any(u => u.UserName.ToUpper() == entity.UserName.ToUpper().Trim()))
                return Error("User with the same name exists! Try another user name.");
            entity.UserName = entity.UserName?.Trim();
            _db.Users.Add(entity);
            _db.SaveChanges();
            return Success(entity.UserName + " is created succesfully.");
        }

        public Service Delete(int id)
        {
            var e = _db.Users.Include(u => u.Blogs).SingleOrDefault(u => u.Id == id);
            if (e == null)
                return Error("User to be deleted can not be found!");
            if (e.Blogs.Any())
                return Error("This user to be deleted has relational blogs.");
            _db.Users.Remove(e);
            _db.SaveChanges();
            return Success("User is deleted.");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).Where(u => u.IsActive).Select(u => new UserModel() { Record = u });
        }

        public Service Update(User entity)
        {
            if (_db.Users.Any(u=> u.Id != entity.Id && u.UserName.ToUpper() == entity.UserName.ToUpper().Trim()))
                return Error("User with the same name exists!");
            var e = _db.Users.SingleOrDefault(u => u.Id == entity.Id);
            if (e == null)
                return Error("Tag to be updated can noube found!");
            e.UserName = entity.UserName?.Trim();
            e.Password = entity.Password?.Trim();
            e.IsActive = entity.IsActive;
            e.RoleId = entity.RoleId;
            
            _db.Users.Update(e);
            _db.SaveChanges();
            return Success(e.UserName + " is updated succesfully.");
        }
    }
}
