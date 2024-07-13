using Microsoft.AspNetCore.Mvc;
using mongomvc.Models;

public class GroupsController : Controller
{
    private readonly GroupService _groupService;

    public GroupsController(GroupService groupService)
    {
        _groupService = groupService;
    }

    public async Task<IActionResult> Index()
    {
        var groups = await _groupService.GetAsync();

        return View(groups);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(GroupViewModel group)
    {
        if (ModelState.IsValid)
        {

            var ss = new Group();
            ss.Name = group.Name;
            await _groupService.CreateAsync(ss);
            return RedirectToAction(nameof(Index));
        }
        return View(group);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var group = await _groupService.GetAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        return View(group);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, Group group)
    {
        if (ModelState.IsValid)
        {
            await _groupService.UpdateAsync(id, group);
            return RedirectToAction(nameof(Index));
        }
        return View(group);
    }

    public async Task<IActionResult> Delete(string id)
    {
        var group = await _groupService.GetAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        return View(group);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _groupService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
