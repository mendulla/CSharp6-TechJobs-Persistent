using System.ComponentModel.DataAnnotations;

namespace TechJobs6Persistent.ViewModels
{
    public class AddEmployerViewModel
    {
        // Name property with validation to ensure it is required
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        // Location property with validation to ensure it is required
        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }
    }
}
