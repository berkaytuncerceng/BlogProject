using System.Security.Claims;
using AutoMapper;
using DataAccess.Repositories.Abstract;
using Application.Dtos;
using DataAccess.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Application.Abstract;


namespace Presentation.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogController(IBlogService blogService, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchTitle, string userName, int? categoryId, DateTime? startDate, DateTime? endDate)
        {
            ViewData["SearchTitle"] = searchTitle;
            ViewData["UserName"] = userName;
            ViewData["CategoryId"] = categoryId;
            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

            ViewBag.Categories = await _categoryService.GetAllAsync();
            var blogs = await _blogService.GetFilteredAsync(searchTitle, userName, categoryId, startDate, endDate);
            return View(blogs);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string? returnUrl = null)
        {
            var blog = await _blogService.GetDetailsByIdAsync(id);
            if (blog == null)
                return NotFound();

            ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Blog");
            return View(blog);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(dto);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _blogService.CreateAsync(dto, userId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var dto = await _blogService.GetEditDtoByIdAsync(id, userId, isAdmin);
            if (dto == null) return Unauthorized();

            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                return View(dto);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            await _blogService.UpdateAsync(dto, userId, isAdmin);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            await _blogService.DeleteAsync(id, userId, isAdmin);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyBlogs()
        {
            var userId = _userManager.GetUserId(User);
            var blogs = await _blogService.GetUserBlogsAsync(userId);
            return View(blogs);
        }
    }
}
