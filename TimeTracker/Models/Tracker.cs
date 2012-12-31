using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimeTracker.Models
{
    /*
     ActionID
    PersonId
    Date
    Time
    Status
     */
    public class Tracker
    {
        [Key]
        public int TrackerID { get; set; }

        [Required]
        [Display(Name="Name | Number")]
        public int PatientID { get; set; }

        [Required]
        [Display(Name = "Date & Time")]
        public DateTime TrakingDateTime { get; set; }
        
        public virtual Patient Patient { get; set; }

        [Required]
        public int StatusId { get; set; }

        
        public virtual Status Status { get; set; }

      

    }
}