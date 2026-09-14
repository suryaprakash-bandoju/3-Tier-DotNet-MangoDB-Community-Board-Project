using CommunityBoard.Models;
using CommunityBoard.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommunityBoard.Controllers;

public class PostsController : Controller
{
    private readonly IPostService _service;

    public PostsController(IPostService service) => _service = service;

    public async Task<IActionResult> Index() => View(await _service.GetAllAsync());

    public IActionResult Create() => View(new Post());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post)
    {
        if (!ModelState.IsValid) return View(post);
        try
        {
            await _service.CreateAsync(post);
            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(post);
        }
    }

    public async Task<IActionResult> Edit(string id)
    {
        var post = await _service.GetByIdAsync(id);
        return post is null ? NotFound() : View(post);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Post post)
    {
        if (!ModelState.IsValid) return View(post);
        try
        {
            var updated = await _service.UpdateAsync(id, post);
            return updated ? RedirectToAction(nameof(Index)) : NotFound();
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(post);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
