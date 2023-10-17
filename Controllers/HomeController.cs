using System.Diagnostics;
using blog.Data;
using Microsoft.AspNetCore.Mvc;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _dbContext.Articles.ToListAsync());
    }
    
    public IActionResult Create()
    {
        return View();
    }

    // Post: Home/Create
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Title,Content")] Article article)
    {
        article.UpdateSlug();
        _dbContext.Add(article);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // GET: Home/Edit/d119ba04-f36c-41b5-90b8-5637c8cd78e3
    public async Task<IActionResult> Edit(Guid id)
    {
        var article = await _dbContext.Articles.FindAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }

    // POST: Home/Edit/d119ba04-f36c-41b5-90b8-5637c8cd78e3
    [HttpPost]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Created,Slug,Title,Content")] Article article)
    {
        article.UpdateUpdated();

        _dbContext.Update(article);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    
    // GET: Home/Read/d119ba04-f36c-41b5-90b8-5637c8cd78e3
    public async Task<IActionResult> Read(Guid id)
    {
        var article = await _dbContext.Articles.FirstOrDefaultAsync(a => a.Id == id);
        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }

    // GET: Home/Delete/d119ba04-f36c-41b5-90b8-5637c8cd78e3
    public async Task<IActionResult> Delete(Guid id)
    {
        var article = await _dbContext.Articles
            .FirstOrDefaultAsync(a => a.Id == id);

        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }
    
    // POST: Home/Delete/d119ba04-f36c-41b5-90b8-5637c8cd78e3
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var article = await _dbContext.Articles.FindAsync(id);
        _dbContext.Articles.Remove(article);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}