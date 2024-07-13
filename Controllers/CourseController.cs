using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mongomvc.Models;
using mongomvc.Service;
using System.Data;

namespace mongomvc.Controllers
{
    public class CourseController : Controller
    {
        private readonly TeacherService _groupService;
        private readonly CourseServise _courseServise;


        public CourseController(TeacherService groupService, CourseServise courseServise)
        {
            _courseServise = courseServise;
            _groupService = groupService;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _courseServise.GetAsync();

            return View(groups);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create()
        {
            ViewBag.Teacher = new SelectList(await _groupService.GetAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(CourseViewModel group)
        {
            if (ModelState.IsValid)
            {

                var ss = new Course();
                ss.Name = group.Name;
                ss.RegistrationNsumber = 0;
                ss.StartTime= group.StartTimeH+":"+ group.StartTimeM;
                ss.EndTime = group.EndTimeH+":"+ group.EndTimeM;
                ss.TeacherId = group.TeacherId;
                ss.Capacity = group.Capacity;
                ss.Day = group.Day;

                await _courseServise.CreateAsync(ss);
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

    }
}
