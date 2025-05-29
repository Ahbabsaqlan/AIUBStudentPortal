// This JavaScript file contains common functionalities for the AIUB Student Portal,
// such as mobile menu toggling and active link highlighting, and modal logic for applications.

document.addEventListener('DOMContentLoaded', function() {
    const menuToggle = document.getElementById('menuToggle');
    const mainNav = document.getElementById('mainNav');
    const navLinks = mainNav.querySelectorAll('a');

    // Function to toggle mobile menu visibility
    if (menuToggle && mainNav) {
        menuToggle.addEventListener('click', function() {
            mainNav.classList.toggle('active');
        });
    }

    // Function to highlight the active navigation link based on the current page URL
    const currentPath = window.location.pathname.split('/').pop(); // Gets the last part of the URL (e.g., "index.html")

    navLinks.forEach(link => {
        // Check if the link's href matches the current page path
        if (link.getAttribute('href').split('/').pop() === currentPath) {
            link.classList.add('active');
        } else if (currentPath === '' && link.getAttribute('href') === '/index.html') {
            // Special handling for the root URL, assuming it maps to index.html
            link.classList.add('active');
        } else if (currentPath === 'Register' && link.getAttribute('href') === '/Register') {
            // Special handling for the Register Razor Page
            link.classList.add('active');
        } else if (currentPath === 'Index' && link.getAttribute('href') === '/Admin/Index') {
            // Special handling for the Admin Index Razor Page
            link.classList.add('active');
        } else if (currentPath === 'Details' && link.getAttribute('href').startsWith('/Admin/Details')) {
            // Special handling for the Admin Details Razor Page
            // Note: This assumes 'Details' is the last part of the path,
            // and the link in the nav is '/Admin/Details' or similar.
            // For exact matches, you might need more specific logic if 'Details' is a common word.
            // For now, we'll rely on the /Admin/Index link being active if on any /Admin page.
            // A more robust solution for admin details would be to check if the current URL starts with /Admin/
            // and activate the Admin Panel link.
            const adminNavLink = document.querySelector('nav ul li a[href="/Admin/Index"]');
            if (adminNavLink) {
                adminNavLink.classList.add('active');
            }
        }
    });

    // Modal functionality for applications.html
    const applicationModal = document.getElementById('applicationModal');
    const closeButton = document.querySelector('.modal .close-button'); // Ensure correct selector
    const applicationCards = document.querySelectorAll('.application-card');
    const modalTitle = document.getElementById('modalTitle');
    const modalApplicationForm = document.getElementById('modalApplicationForm');
    const reasonDetailsField = document.getElementById('reasonDetails'); // Get the common reason/details field

    // Only proceed if the modal elements exist on the page (i.e., we are on applications.html)
    if (applicationModal && closeButton && applicationCards.length > 0 && modalTitle && modalApplicationForm) {
        applicationCards.forEach(card => {
            card.addEventListener('click', function() {
                const appType = this.dataset.applicationType;
                let title = '';
                let specificFieldsHtml = '';

                // Clear any previously added dynamic fields
                const existingDynamicFields = modalApplicationForm.querySelectorAll('.dynamic-field-group');
                existingDynamicFields.forEach(field => field.remove());

                // Reset common fields if necessary
                if (reasonDetailsField) {
                    reasonDetailsField.value = ''; // Clear reason details
                }

                switch(appType) {
                    case 'transcript':
                        title = 'Official Transcript Request';
                        specificFieldsHtml = `
                            <div class="form-group dynamic-field-group">
                                <label for="numCopies">Number of Copies:</label>
                                <input type="number" id="numCopies" name="numCopies" min="1" value="1" class="form-control">
                            </div>
                            <div class="form-group dynamic-field-group">
                                <label for="deliveryMethod">Delivery Method:</label>
                                <select id="deliveryMethod" name="deliveryMethod" class="form-control">
                                    <option value="pickup">Self-Pickup</option>
                                    <option value="mail">Mail to Address</option>
                                </select>
                            </div>
                        `;
                        break;
                    case 'certificate':
                        title = 'Degree Certificate Request';
                        specificFieldsHtml = `
                            <div class="form-group dynamic-field-group">
                                <label for="programName">Program Name:</label>
                                <input type="text" id="programName" name="programName" value="B.Sc. in Computer Science & Engineering" readonly class="form-control">
                            </div>
                            <div class="form-group dynamic-field-group">
                                <label for="graduationSemester">Graduation Semester:</label>
                                <input type="text" id="graduationSemester" name="graduationSemester" placeholder="e.g., Fall 2024" class="form-control">
                            </div>
                        `;
                        break;
                    case 'leave':
                        title = 'Leave of Absence Request';
                        specificFieldsHtml = `
                            <div class="form-group dynamic-field-group">
                                <label for="leaveStartDate">Start Date:</label>
                                <input type="date" id="leaveStartDate" name="leaveStartDate" class="form-control">
                            </div>
                            <div class="form-group dynamic-field-group">
                                <label for="leaveEndDate">End Date:</label>
                                <input type="date" id="leaveEndDate" name="leaveEndDate" class="form-control">
                            </div>
                            <div class="form-group dynamic-field-group">
                                <label for="leaveType">Type of Leave:</label>
                                <select id="leaveType" name="leaveType" class="form-control">
                                    <option value="medical">Medical</option>
                                    <option value="personal">Personal</option>
                                    <option value="financial">Financial</option>
                                    <option value="other">Other</option>
                                </select>
                            </div>
                        `;
                        break;
                    case 're-admission':
                        title = 'Re-Admission Application';
                        specificFieldsHtml = `
                            <div class="form-group dynamic-field-group">
                                <label for="lastAttendedSemester">Last Attended Semester:</label>
                                <input type="text" id="lastAttendedSemester" name="lastAttendedSemester" placeholder="e.g., Spring 2023" class="form-control">
                            </div>
                            <div class="form-group dynamic-field-group">
                                <label for="reAdmissionSemester">Desired Re-Admission Semester:</label>
                                <input type="text" id="reAdmissionSemester" name="reAdmissionSemester" placeholder="e.g., Summer 2025" class="form-control">
                            </div>
                        `;
                        break;
                }

                modalTitle.textContent = title;
                // Insert specific fields before the submit button
                const submitButton = modalApplicationForm.querySelector('.button');
                const tempDiv = document.createElement('div');
                tempDiv.innerHTML = specificFieldsHtml;
                while (tempDiv.firstChild) {
                    modalApplicationForm.insertBefore(tempDiv.firstChild, submitButton);
                }

                applicationModal.style.display = 'flex'; // Show modal
            });
        });

        // Close modal when the close button is clicked
        closeButton.addEventListener('click', function() {
            applicationModal.style.display = 'none';
            // Clear dynamically added fields when closing
            const dynamicFields = modalApplicationForm.querySelectorAll('.dynamic-field-group');
            dynamicFields.forEach(field => field.remove());
        });

        // Close modal if clicked outside the modal content
        window.addEventListener('click', function(event) {
            if (event.target == applicationModal) {
                applicationModal.style.display = 'none';
                // Clear dynamically added fields when closing
                const dynamicFields = modalApplicationForm.querySelectorAll('.dynamic-field-group');
                dynamicFields.forEach(field => field.remove());
            }
        });

        // Handle form submission (client-side only for this design)
        modalApplicationForm.addEventListener('submit', function(event) {
            event.preventDefault(); // Prevent default form submission
            // In a real application, you would send this data to the backend via Fetch API or XMLHttpRequest.
            // For now, just simulate success and close the modal.
            console.log('Application Submitted:', new FormData(this));
            // Using alert for simplicity, but in a real app, replace with a custom message box or toast notification.
            alert('Your application has been submitted successfully!');
            applicationModal.style.display = 'none';
            // Clear dynamically added fields after submission
            const dynamicFields = modalApplicationForm.querySelectorAll('.dynamic-field-group');
            dynamicFields.forEach(field => field.remove());
        });
    }
});
