﻿using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TagService : Service, IService<Tag, TagModel>
    {
        public TagService(Db db) : base(db)
        {
        }

        public Service Create(Tag entity)
        {
            if (_db.Tags.Any(t => t.Name.ToUpper() == entity.Name.ToUpper().Trim()))
                return Error("Tag with the same name exists!");
            entity.Name = entity.Name?.Trim();
            _db.Tags.Add(entity);
            _db.SaveChanges();
            return Success(entity.Name + " is created succesfully.");
        }

        public Service Delete(int id)
        {
            var e = _db.Tags.Include(b => b.BlogTags).SingleOrDefault(t => t.Id == id);
            if (e == null)
                return Error("Tag to be deleted can not be found!");
            _db.BlogTags.RemoveRange(e.BlogTags);
            _db.Tags.Remove(e);
            _db.SaveChanges();
            return Success("Tag is deleted.");
        }

        public Service Update(Tag entity)
        {
            if (_db.Tags.Any(t => t.Id != entity.Id && t.Name.ToUpper() == entity.Name.ToUpper().Trim()))
                return Error("Tag with the same name exists!");
            var e = _db.Tags.Include(b => b.BlogTags).SingleOrDefault(t => t.Id == entity.Id);
            if (e == null)
                return Error("Tag to be updated can not be found!");
            _db.BlogTags.RemoveRange(e.BlogTags);
            e.Name = entity.Name?.Trim();
            _db.Tags.Update(e);
            _db.SaveChanges();
            return Success("Tag is updated succesfully.");
        }

        IQueryable<TagModel> IService<Tag, TagModel>.Query()
        {
            return _db.Tags
                    .Include(t => t.BlogTags).ThenInclude(bt => bt.Blog)
                    .OrderBy(t => t.Name)
                    .Select(t => new TagModel { Record = t });
        }
    }
}
