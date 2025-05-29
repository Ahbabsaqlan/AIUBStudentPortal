// This is the code-behind file for the student registration Razor Page.
// It handles the logic for displaying the form and processing form submissions.
using System;
using System.Collections.Generic;
using System.Linq; // Required for LINQ operations like .FirstOrDefault()
using System.Threading.Tasks; // For asynchronous operations, though not strictly used with in-memory list
using Microsoft.AspNetCore.Mvc; // For IActionResult, Page(), RedirectToPage()
using Microsoft.AspNetCore.Mvc.RazorPages; // For PageModel
using AIUBStudentPortal.Models; // Import the StudentProfile model

namespace AIUBStudentPortal.Pages
{
    public class RegisterModel : PageModel
    {
        // [BindProperty] attribute enables automatic binding of form data to this property
        // when the form is submitted (OnPost method).
        [BindProperty]
        public StudentProfile StudentProfile { get; set; }

        // In-memory list to simulate a database for storing student profiles.
        // In a real application, this would be replaced with a database context (e.g., DbContext from Entity Framework Core).
        // It's static to ensure the list persists across requests within the same application instance.
        private static List<StudentProfile> _studentProfiles = new List<StudentProfile>();
        // Simple counter to generate unique IDs for new profiles in the in-memory list.
        private static int _nextId = 1;

        // Static constructor to initialize the list with some dummy data when the class is first loaded.
        // This is useful for testing and demonstration purposes.
        static RegisterModel()
        {
            // Add some initial profiles if the list is empty.
            if (!_studentProfiles.Any())
            {
                _studentProfiles.Add(new StudentProfile { Id = _nextId++, FullName = "Alice Wonderland", StudentId = "20-12345-1", Email = "alice.w@example.com", PhoneNumber = "01711223344", Status = "Pending", SubmissionDate = System.DateTime.Now.AddDays(-5) });
                _studentProfiles.Add(new StudentProfile { Id = _nextId++, FullName = "Bob The Builder", StudentId = "21-67890-2", Email = "bob.b@example.com", PhoneNumber = "01822334455", Status = "Approved", SubmissionDate = System.DateTime.Now.AddDays(-10) });
                _studentProfiles.Add(new StudentProfile { Id = _nextId++, FullName = "Charlie Chaplin", StudentId = "19-11223-3", Email = "charlie.c@example.com", PhoneNumber = "01933445566", Status = "Pending", SubmissionDate = System.DateTime.Now.AddDays(-2) });
            }
        }

        // OnGet method is executed when the page is requested via an HTTP GET verb.
        public void OnGet()
        {
            // Initialize a new StudentProfile object for the form.
            // This ensures the form fields are empty when the page is first loaded.
            StudentProfile = new StudentProfile();
        }

        // OnPost method is executed when the form is submitted via an HTTP POST verb.
        public IActionResult OnPost()
        {
            // Check if the submitted model (StudentProfile) is valid based on data annotations.
            if (!ModelState.IsValid)
            {
                // If there are validation errors, return the same page to display error messages.
                return Page();
            }

            // Assign a unique ID to the new student profile.
            StudentProfile.Id = _nextId++;
            // Set the submission date to the current date and time.
            StudentProfile.SubmissionDate = DateTime.Now;
            // Set the initial status of the profile to "Pending".
            StudentProfile.Status = "Pending";

            // Add the newly created student profile to the in-memory list.
            _studentProfiles.Add(StudentProfile);

            // In a real application, you would save StudentProfile to a database here.
            // Example using Entity Framework Core:
            // _dbContext.StudentProfiles.Add(StudentProfile);
            // await _dbContext.SaveChangesAsync();

            // Redirect to the Admin/Index page after successful registration.
            // A message is passed via query string to be displayed on the Admin page.
            return RedirectToPage("/Admin/Index", new { message = "Registration successful! Awaiting admin approval." });
        }

        // Helper method to provide access to the static list of student profiles.
        // This is used by other Razor Pages (like Admin/Index and Admin/Details) to retrieve data.
        public static List<StudentProfile> GetStudentProfiles()
        {
            return _studentProfiles;
        }
    }
}
