﻿using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RoleService : Service, IService<Role, RoleModel>
    {
        public RoleService(Db db) : base(db)
        {
        }

        public Service Create(Role entity)
        {
            if (_db.Roles.Any(r => r.Name.ToUpper() == entity.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            entity.Name = entity.Name?.Trim();
            _db.Roles.Add(entity);
            _db.SaveChanges();
            return Success(entity.Name + " is created succesfully.");
        }

        public Service Delete(int id)
        {
            var e = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (e == null)
                return Error("Role to be deleted can not be found!");
            if (e.Users.Any())
                return Error("This role to be deleted has relational users.");
            _db.Roles.Remove(e);
            _db.SaveChanges();
            return Success("Role is deleted.");
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(r => r.Name).Select(r => new RoleModel() { Record = r });
        }

        public Service Update(Role entity)
        {
            if (_db.Roles.Any(r => r.Id != entity.Id && r.Name.ToUpper() == entity.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            var e = _db.Roles.SingleOrDefault(r => r.Id == entity.Id);
            if (e == null)
                Error("Role to be updated can not be found!");
            e.Name = entity.Name?.Trim();
            _db.Roles.Update(e);
            _db.SaveChanges();
            return Success("Role is updated succesfully.");
        }
    }
}
