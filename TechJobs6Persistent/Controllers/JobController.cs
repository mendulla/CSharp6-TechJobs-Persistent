﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechJobs6Persistent.Data;
using TechJobs6Persistent.Models;
using TechJobs6Persistent.ViewModels;

namespace TechJobs6Persistent.Controllers
{
    public class JobController : Controller
    {
        private JobDbContext context;

        public JobController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();
            return View(jobs);
        }

        public IActionResult Add()
        {
            // Get the list of all employers to be displayed in the dropdown
            List<Employer> employers = context.Employers.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers);
            return View(addJobViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddJobViewModel addJobViewModel)
        {
            if (ModelState.IsValid)
            {
                Employer employer = context.Employers.Find(addJobViewModel.EmployerId);

                Job newJob = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = employer
                };

                context.Jobs.Add(newJob);
                context.SaveChanges();

                return Redirect("/Job"); // Redirect to the Job index page
            }

            List<Employer> employers = context.Employers.ToList();
            addJobViewModel.Employers = employers.Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();

            return View(addJobViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.jobs = context.Jobs.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] jobIds)
        {
            foreach (int jobId in jobIds)
            {
                Job theJob = context.Jobs.Find(jobId);
                context.Jobs.Remove(theJob);
            }

            context.SaveChanges();

            return Redirect("/Job");
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Include(j => j.Skills)
                .Single(j => j.Id == id);

            JobDetailViewModel jobDetailViewModel = new JobDetailViewModel(theJob);
            return View(jobDetailViewModel);
        }
    }
}
