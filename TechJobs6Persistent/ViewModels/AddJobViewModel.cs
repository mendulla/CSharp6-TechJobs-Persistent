using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechJobs6Persistent.Models;

namespace TechJobs6Persistent.ViewModels
{
    public class AddJobViewModel
    {
        // Required field for job name
        [Required(ErrorMessage = "Job name is required")]
        public string? Name { get; set; }

        // Required field for employer selection
        [Required(ErrorMessage = "Employer is required")]
        public int? EmployerId { get; set; }

        // List of employers for dropdown
        public List<SelectListItem>? Employers { get; set; }

        // Default constructor
        public AddJobViewModel()
        {
        }

        // Constructor to initialize the list of employers
        public AddJobViewModel(IEnumerable<Employer> employers)
        {
            Employers = new List<SelectListItem>();

            // Populate the SelectListItem with employer data
            foreach (var employer in employers)
            {
                Employers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }
        }
    }
}
