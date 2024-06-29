using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechJobs6Persistent.Data;
using TechJobs6Persistent.Models;
using TechJobs6Persistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobs6Persistent.Controllers
{
    public class EmployerController : Controller
    {
        // Private variable to hold the database context
        private JobDbContext context;

        // Constructor to initialize the database context
        public EmployerController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            // Retrieve all employers from the database
            List<Employer> employers = context.Employers.ToList();
            // Pass the list of employers to the view
            return View(employers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Create a new instance of AddEmployerViewModel
            AddEmployerViewModel addEmployerViewModel = new AddEmployerViewModel();
            // Pass the ViewModel to the view
            return View(addEmployerViewModel);
        }

        [HttpPost]
        public IActionResult ProcessCreateEmployerForm(AddEmployerViewModel addEmployerViewModel)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Create a new Employer object and populate it with data from the ViewModel
                Employer newEmployer = new Employer
                {
                    Name = addEmployerViewModel.Name,
                    Location = addEmployerViewModel.Location
                };

                // Add the new employer to the database and save changes
                context.Employers.Add(newEmployer);
                context.SaveChanges();

                // Redirect to the Index action method
                return Redirect("/Employer");
            }

            // If model state is not valid, return to the Create view with the ViewModel
            return View("Create", addEmployerViewModel);
        }

        public IActionResult About(int id)
        {
            // Find the employer with the specified id in the database
            Employer employer = context.Employers.Find(id);
            // Pass the employer to the view
            return View(employer);
        }
    }
}
