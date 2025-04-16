using Application.Abstract;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Policy;

[Authorize]
public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly ICategoryService _categoryService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<BlogController> _logger;

    public BlogController(IBlogService blogService, ICategoryService categoryService, UserManager<IdentityUser> userManager, ILogger<BlogController> logger)
    {
        _blogService = blogService;
        _categoryService = categoryService;
        _userManager = userManager;
        _logger = logger;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index(string searchTitle, string userName, int? categoryId, DateTime? startDate, DateTime? endDate)
    {
        _logger.LogInformation("Blog listesi filtreleniyor...");
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
        _logger.LogInformation("Detay görüntüleniyor. Blog ID: {BlogId}", id);
        var blog = await _blogService.GetDetailsByIdAsync(id);
        if (blog == null)
        {
            _logger.LogWarning("Blog bulunamadı. ID: {BlogId}", id);
            return NotFound();
        }

        ViewBag.ReturnUrl = returnUrl ?? Url.Action("Index", "Blog");
        return View(blog);
    }

    public async Task<IActionResult> Create()
    {
        _logger.LogInformation("Blog oluşturma formu açıldı.");
        ViewBag.Categories = await _categoryService.GetAllAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BlogCreateDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Blog oluşturma formunda doğrulama hatası.");
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(dto);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _blogService.CreateAsync(dto, userId);
        _logger.LogInformation("Blog başarıyla oluşturuldu. Başlık: {Title}", dto.Title);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = User.IsInRole("Admin");

        var dto = await _blogService.GetEditDtoByIdAsync(id, userId, isAdmin);
        if (dto == null)
        {
            _logger.LogWarning("Yetkisiz düzenleme girişimi. BlogID: {BlogId}, UserID: {UserId}, isAdmin: {IsAdmin}", id, userId, isAdmin);
            return Unauthorized();
        }

        _logger.LogInformation("Blog düzenleme formu açıldı. BlogID: {BlogId}", id);
        ViewBag.Categories = await _categoryService.GetAllAsync();
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BlogEditDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Blog düzenleme formunda doğrulama hatası. BlogID: {BlogId}", dto.Id);
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(dto);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = User.IsInRole("Admin");

        await _blogService.UpdateAsync(dto, userId, isAdmin);
        _logger.LogInformation("Blog güncellendi. BlogID: {BlogId}", dto.Id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = User.IsInRole("Admin");

        await _blogService.DeleteAsync(id, userId, isAdmin);
        _logger.LogInformation("Blog silindi. BlogID: {BlogId}", id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> MyBlogs()
    {
        var userId = _userManager.GetUserId(User);
        _logger.LogInformation("Kullanıcının blogları görüntüleniyor. UserID: {UserId}", userId);

        var blogs = await _blogService.GetUserBlogsAsync(userId);
        return View(blogs);
    }
}
