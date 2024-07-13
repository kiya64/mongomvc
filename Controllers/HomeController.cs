using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using mongomvc.Models;
using mongomvc.Service;
using System.Diagnostics;

namespace mongomvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentService _studentService;
        private readonly TeacherService _teacherService;
        private readonly DegreeService _degreeService;

        public HomeController(ILogger<HomeController> logger, StudentService studentService, TeacherService teacherService, DegreeService degreeService)
        {
            _logger = logger;
            _studentService = studentService;
            _teacherService = teacherService;
            _degreeService = degreeService;
        }

        public  IActionResult Index()
        {
      

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> AboutFaculty()
        {
            var x = await _studentService.GetAsync();
            var xxx = x.Count;
            var y= await _teacherService.GetAsync();
            var yyy= y.Count;
          var xxa= await _degreeService.GetAsync();

            return View(new AboutFaculty { countStudent=xxx,countTeacher=yyy,degrees=xxa});
        }


        public IActionResult EducationalGroups()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult FacultyMembers()
        {
            return View();
        }
        public IActionResult DegreePrograms()
        {
            return View();
        }
        public IActionResult FacultyMemberDetails(string id)
        {
            ViewData["Id"] = id;
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult PhotoAlbum()
        {
            return View();
        }
    }
}