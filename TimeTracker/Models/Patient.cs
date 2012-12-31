using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracker.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string MiddleName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        public bool IsLocked { get; set; }

        public string PersonType { get; set; }


    }

    public static class PersonType
    {
        public static SelectList AllTypes()
        {
            List<SelectListItem> dictionary = new List<SelectListItem>();
            dictionary.Add(new SelectListItem() { Text = "Patient", Value = "success" });
            dictionary.Add(new SelectListItem() { Text = "Staff", Value = "info" });
            dictionary.Add(new SelectListItem() { Text = "Other", Value = "Orange" });
            
            return new SelectList(dictionary,"Value","Text");
        }
    }
}