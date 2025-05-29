// This file defines the data model for a student profile, including validation attributes.
using System;
using System.ComponentModel.DataAnnotations; // Namespace for data validation attributes

namespace AIUBStudentPortal.Models
{
    public class StudentProfile
    {
        // Unique identifier for each student profile.
        public int Id { get; set; }

        // Student's full name. It's required and has a maximum length of 100 characters.
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        // Student's ID. Required and limited to 20 characters.
        [Required(ErrorMessage = "Student ID is required.")]
        [StringLength(20, ErrorMessage = "Student ID cannot exceed 20 characters.")]
        public string StudentId { get; set; }

        // Student's email address. Required and validated as an email format.
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        // Student's phone number. Validated as a phone number format (optional).
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        // Current status of the student profile (e.g., "Pending", "Approved", "Disapproved").
        // Defaults to "Pending" upon creation.
        public string Status { get; set; } = "Pending";

        // The date and time when the profile was submitted. Defaults to the current time.
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
