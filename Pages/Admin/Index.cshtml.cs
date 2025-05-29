// This is the code-behind file for the Admin Panel's index page.
// It retrieves and displays a list of all registered student profiles.
using System.Collections.Generic; // For List<T>
using System.Linq; // For LINQ methods like .ToList(), .Any()
using Microsoft.AspNetCore.Mvc; // For IActionResult
using Microsoft.AspNetCore.Mvc.RazorPages; // For PageModel
using AIUBStudentPortal.Models; // Import the StudentProfile model

namespace AIUBStudentPortal.Pages.Admin
{
    public class IndexModel : PageModel
    {
        // Property to hold the list of student profiles to be displayed on the page.
        public IList<StudentProfile> StudentProfiles { get; set; }
        // Property to display a message, typically from a redirect after an action (e.g., successful registration).
        public string Message { get; set; }

        // This static list is shared across all instances of RegisterModel and Admin/IndexModel
        // to simulate a persistent data store without a real database.
        // In a production environment, this would be replaced by a database context.
        private static List<StudentProfile> _studentProfiles = new List<StudentProfile>();

        // Static constructor to initialize the list with some dummy data when the class is first loaded.
        // This ensures there's some data to display even on the first run.
        static IndexModel()
        {
            // Add some initial dummy profiles if the list is empty.
            if (!_studentProfiles.Any())
            {
                _studentProfiles.Add(new StudentProfile { Id = 1, FullName = "Alice Smith", StudentId = "20-12345-1", Email = "alice@example.com", PhoneNumber = "01711223344", Status = "Pending", SubmissionDate = System.DateTime.Now.AddDays(-5) });
                _studentProfiles.Add(new StudentProfile { Id = 2, FullName = "Bob Johnson", StudentId = "21-67890-2", Email = "bob@example.com", PhoneNumber = "01822334455", Status = "Approved", SubmissionDate = System.DateTime.Now.AddDays(-10) });
                _studentProfiles.Add(new StudentProfile { Id = 3, FullName = "Charlie Brown", StudentId = "19-11223-3", Email = "charlie@example.com", PhoneNumber = "01933445566", Status = "Pending", SubmissionDate = System.DateTime.Now.AddDays(-2) });
            }
        }

        // OnGet method is executed when the Admin/Index page is requested via an HTTP GET verb.
        // It takes an optional 'message' parameter from the query string.
        public void OnGet(string message)
        {
            // Retrieve all student profiles from the static in-memory list.
            StudentProfiles = _studentProfiles.ToList();
            // Assign the message from the query string to the Message property for display.
            Message = message;
        }

        // Helper method to provide external access to the static list of student profiles.
        // This is crucial for the Admin/Details page to find specific profiles.
        public static List<StudentProfile> GetStudentProfiles()
        {
            return _studentProfiles;
        }
    }
}
