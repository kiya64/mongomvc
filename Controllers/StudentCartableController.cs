using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mongomvc.Service;
using System.Data;

namespace mongomvc.Controllers
{
    [Authorize(Roles = "Student")]

    public class StudentCartableController : Controller
    {
        private readonly ChoiceOfCoursesServise _choiceOfCoursesServise;
        private readonly CourseServise _courseServise;

        public StudentCartableController(ChoiceOfCoursesServise choiceOfCoursesServise, CourseServise courseServise)
        {
            _choiceOfCoursesServise = choiceOfCoursesServise;
            _courseServise = courseServise;
        }
        public async Task<IActionResult> Index()
        {
            var x = await _choiceOfCoursesServise.GetAsync();
            var y = await _choiceOfCoursesServise.GetAsync(x);
            return View(y);
        }
        public async Task<IActionResult> EntekhabVahed()
        {
          var x = await  _courseServise.GetAsync();
            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> EntekhabVahed(string id)
        {
            var x =await _choiceOfCoursesServise.AddToList(id);
            if (x == "200") {
               
                return RedirectToAction(nameof(EntekhabVahed));
            }
            else
            {
                ViewBag.AlertMessage = x;
                var courses = await _courseServise.GetAsync();
                return View(courses);
            }
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            await _choiceOfCoursesServise.RemoveAsync(id);
           
            return RedirectToAction(nameof(Index));
        }
    }
}
