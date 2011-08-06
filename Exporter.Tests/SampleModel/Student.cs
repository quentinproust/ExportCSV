using System;
using System.ComponentModel.DataAnnotations;

namespace Exporter.Tests.SampleModel
{
    public class Student
    {
        public Guid Id { get; set; }

        public string OfficialId { get; set; }
        [Display(Name="Prénom")]
        public string FirstName { get; set; }
        [Display(Name = "Nom de famille")]
        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
        public long Age { 
            get { return DateTime.Now.Subtract(Birthday).Days/365; } 
        }

        public bool IsScholar { get; set; }
    }
}
