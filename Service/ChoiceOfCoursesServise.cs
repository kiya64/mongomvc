using MongoDB.Driver;
using mongomvc.Models;
using System.Security.Claims;

namespace mongomvc.Service
{
    public class ChoiceOfCoursesServise
    {
        private readonly IMongoCollection<Enrollment> _groups;
        private readonly CourseServise _Course;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChoiceOfCoursesServise(IMongoClient mongoClient, IHttpContextAccessor httpContextAccessor, CourseServise courseServise)
        {
            var database = mongoClient.GetDatabase("WebProject");
            _groups = database.GetCollection<Enrollment>("Enrollment");
            _Course = courseServise;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Enrollment>> GetAsync() {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User  not found.");
            }

            var group = await _groups.Find(group => group.UserId == userId).ToListAsync();
            return group;
        }
        public async Task<Enrollment> GetAsync(string id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User  not found.");
            }

            var group = await _groups.Find<Enrollment>(group => group.CourseId == id).FirstOrDefaultAsync();
            return group;
        }
        public async Task<List<Course>> GetAsync(List<Enrollment> enrollments)
        {
            var result= new List<Course>();

            foreach (var enrollment in enrollments)
            {
                result.Add(await _Course.GetAsync(enrollment.CourseId));

            }

            return result;
        }

        public async Task<string> AddToList(string Id)
        {
            var x = await _Course.GetAsync(Id);
            var ss = await GetAsync();
            var sa = ss.Find(a => a.CourseId == Id);
            if (x.RegistrationNsumber != x.Capacity)
            {



                if (sa != null)
                {
                    return "درس قبلا انتخاب شده";

                }

                foreach (var item in ss)
                {
                    var x01 = await _Course.GetAsync(item.CourseId);
                    if (x01.Day == x.Day)
                    {
                        if (String.Compare(x01.StartTime, x.EndTime) <= 0 && String.Compare(x01.EndTime, x.StartTime) >= 0)
                        {
                            return "مشکل تداخل وجود دارد";

                        }
                    }
                }
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                await CreateAsync(new Enrollment { CourseId = x.Id, UserId = userId });
                x.RegistrationNsumber++;
                await _Course.UpdateAsync(x.Id, x);

                return "200";
            }
            else
            {
                return "عدم وجود ظرفیت";
            }
        }
        public async Task CreateAsync(Enrollment group) {


            await _groups.InsertOneAsync(group);
        }

        public async Task RemoveAsync(string id) { 
            var x = await GetAsync(id);
        var a=    await _Course.GetAsync(x.CourseId);
            a.RegistrationNsumber --;
            await _Course.UpdateAsync(a.Id, a);
        await _groups.DeleteOneAsync(group => group.Id == x.Id);
        }




    }
}
