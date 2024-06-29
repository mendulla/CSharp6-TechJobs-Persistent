using System;

namespace TechJobs6Persistent.Models
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        // List of jobs associated with the employer
        public List<Job> Jobs { get; set; }

        // Constructor to initialize name and location
        public Employer(string name, string location)
        {
            Name = name;
            Location = location;
        }

        // Parameterless constructor
        public Employer()
        {
        }
    }
}
