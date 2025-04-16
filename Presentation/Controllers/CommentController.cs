using Application.Abstract;
using Application.Dtos;
using DataAccess.Repositories.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _commentService.AddAsync(dto);
            }
            return RedirectToAction("Details", "Blog", new { id = dto.BlogId });
        }
    }
}
