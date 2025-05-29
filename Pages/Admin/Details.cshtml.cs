// This is the code-behind file for the Admin Panel's profile details page.
// It allows administrators to view a specific student profile and approve or disapprove it.
using System.Linq; // For LINQ methods like .FirstOrDefault()
using Microsoft.AspNetCore.Mvc; // For IActionResult, NotFound(), RedirectToPage()
using Microsoft.AspNetCore.Mvc.RazorPages; // For PageModel
using AIUBStudentPortal.Models; // Import the StudentProfile model

namespace AIUBStudentPortal.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        // Property to hold the single student profile whose details are being viewed.
        public StudentProfile StudentProfile { get; set; }

        // OnGet method is executed when the Admin/Details page is requested via an HTTP GET verb.
        // It expects an 'id' parameter in the route (e.g., /Admin/Details/1).
        public IActionResult OnGet(int id)
        {
            // Retrieve the student profile from the static in-memory list using the provided ID.
            StudentProfile = RegisterModel.GetStudentProfiles().FirstOrDefault(p => p.Id == id);

            // If no profile is found with the given ID, return a 404 Not Found result.
            if (StudentProfile == null)
            {
                return NotFound();
            }

            // If a profile is found, return the Page() to display its details.
            return Page();
        }

        // OnPostApprove method is executed when the "Approve" button is submitted via HTTP POST.
        // It updates the status of the specified student profile to "Approved".
        public IActionResult OnPostApprove(int id)
        {
            // Find the profile to approve by its ID.
            var profile = RegisterModel.GetStudentProfiles().FirstOrDefault(p => p.Id == id);
            if (profile != null)
            {
                profile.Status = "Approved"; // Update the status
                // In a real application, you would update the database here.
                // Example: _dbContext.StudentProfiles.Update(profile);
                //          await _dbContext.SaveChangesAsync();
            }
            // Redirect back to the Admin/Index page with a success message.
            return RedirectToPage("./Index", new { message = $"Profile for {profile?.FullName} (ID: {profile?.StudentId}) has been Approved." });
        }

        // OnPostDisapprove method is executed when the "Disapprove" button is submitted via HTTP POST.
        // It updates the status of the specified student profile to "Disapproved".
        public IActionResult OnPostDisapprove(int id)
        {
            // Find the profile to disapprove by its ID.
            var profile = RegisterModel.GetStudentProfiles().FirstOrDefault(p => p.Id == id);
            if (profile != null)
            {
                profile.Status = "Disapproved"; // Update the status
                // In a real application, you would update the database here.
                // Example: _dbContext.StudentProfiles.Update(profile);
                //          await _dbContext.SaveChangesAsync();
            }
            // Redirect back to the Admin/Index page with a disapproval message.
            return RedirectToPage("./Index", new { message = $"Profile for {profile?.FullName} (ID: {profile?.StudentId}) has been Disapproved." });
        }
    }
}
