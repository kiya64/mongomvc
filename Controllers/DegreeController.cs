using Microsoft.AspNetCore.Mvc;
using mongomvc.Models;
using mongomvc.Service;

namespace mongomvc.Controllers
{
    public class DegreeController : Controller
    {
        private readonly DegreeService _groupService;

        public DegreeController(DegreeService groupService)
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
        public async Task<IActionResult> Create(DegreeViewModel group)
        {
            if (ModelState.IsValid)
            {

                var ss = new Group();
                ss.Name = group.Name;
                await _groupService.CreateAsync(new Degree { Name= group.Name});
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

    }
}
