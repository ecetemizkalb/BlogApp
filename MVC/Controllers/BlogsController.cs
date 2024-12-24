#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    public class BlogsController : MvcController
    {
        private readonly IService<Blog, BlogModel> _blogService;
        private readonly IService<User, UserModel> _userService;
        private readonly IService<Tag, TagModel> _tagService;

        public BlogsController(
            IService<Blog, BlogModel> blogService,
            IService<User, UserModel> userService,
            IService<Tag, TagModel> tagService
        )
        {
            _blogService = blogService;
            _userService = userService;
            _tagService = tagService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var list = _blogService.Query().ToList();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var item = _blogService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            ViewData["UserId"] = new SelectList(_userService.Query()
            .Where(u => u.Record.Role.Name == "Admin" || u.Record.Role.Name == "Creator")
            .ToList(), "Record.Id", "UserName");
            ViewBag.TagIds = new MultiSelectList(_tagService.Query().ToList(), "Record.Id", "Name");
        }

        [Authorize(Roles = "Admin, Creator")]
        public IActionResult Create()
        {
            SetViewData();
            return View(new BlogModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Creator")]
        public IActionResult Create(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                var result = _blogService.Create(blog.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = blog.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(blog);
        }

        [Authorize(Roles = "Admin, Creator")]
        public IActionResult Edit(int id)
        {
            var item = _blogService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Creator")]
        public IActionResult Edit(BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                var result = _blogService.Update(blog.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = blog.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(blog);
        }

        [Authorize(Roles = "Admin, Creator")]
        public IActionResult Delete(int id)
        {
            var item = _blogService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Creator")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _blogService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}

