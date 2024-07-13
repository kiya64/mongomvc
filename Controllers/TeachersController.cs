using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using mongomvc.Models;

public class TeachersController : Controller
{
    private readonly TeacherService _teacherService;
    private readonly GroupService _groupService;

    public TeachersController(TeacherService teacherService, GroupService groupService)
    {
        _teacherService = teacherService;
        _groupService = groupService;
    }

    public async Task<IActionResult> Index()
    {
        var teachers = await _teacherService.GetAsync();
        return View(teachers);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Groups = new SelectList(await _groupService.GetAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TeacherViewModel teacher)
    {
        if (ModelState.IsValid)
        {
            using var stream = new MemoryStream();
            await teacher. DataFile.CopyToAsync(stream);

            var teacherModel = new Teacher
            {

                FileName = teacher.DataFile.FileName,
                ContentType = teacher.DataFile.ContentType,
                Data = stream.ToArray(),
                Name = teacher.Name,
                GroupId = teacher.GroupId
            };


            await _teacherService.CreateAsync(teacherModel);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Groups = new SelectList(await _groupService.GetAsync(), "Id", "Name");
        return View(teacher);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var teacher = await _teacherService.GetAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }
        ViewBag.Groups = new SelectList(await _groupService.GetAsync(), "Id", "Name", teacher.GroupId);
        return View(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            await _teacherService.UpdateAsync(id, teacher);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Groups = new SelectList(await _groupService.GetAsync(), "Id", "Name", teacher.GroupId);
        return View(teacher);
    }

    public async Task<IActionResult> Delete(string id)
    {
        var teacher = await _teacherService.GetAsync(id);
        if (teacher == null)
        {
            return NotFound();
        }
        return View(teacher);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _teacherService.RemoveAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
