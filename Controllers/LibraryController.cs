using Microsoft.AspNetCore.Mvc;
using Lr4.Net.Models;

namespace Lr4.Net.Controllers
{
    public class LibraryController : Controller
    {
        private readonly IConfiguration _configuration;

        public LibraryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // /Library
        public IActionResult Index()
        {
            return Content("Welcome to the Library!");
        }

        // /Library/Books
        public IActionResult Books()
        {
            var books = _configuration.GetSection("Books").Get<List<string>>();
            if (books == null || !books.Any())
            {
                return Content("No books found.");
            }

            string result = "Books in the library:\n";
            foreach (var book in books)
            {
                result += book + "\n";
            }

            return Content(result);
        }

        // /Library/Profile
        public IActionResult Profile(int? id)
        {
            var users = _configuration.GetSection("Users").Get<List<UserProfile>>();
            if (users == null || !users.Any())
            {
                return Content("No user profiles found.");
            }

            if (id.HasValue && id >= 0 && id < users.Count)
            {
                var user = users[id.Value];
                return Content($"User Profile:\nName: {user.Name}\nEmail: {user.Email}\nAge: {user.Age}");
            }

            var defaultUser = users.FirstOrDefault();
            return Content($"Default User Profile:\nName: {defaultUser?.Name}\nEmail: {defaultUser?.Email}\nAge: {defaultUser?.Age}");
        }
    }
}