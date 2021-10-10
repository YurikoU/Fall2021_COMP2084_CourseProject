using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fall2021_COMP2084_CourseProject.Models
{
    public class City
    {
        //Primary key
        public int Id { get; set; }

        [DataType(DataType.Date)] //Show only the date
        [Display(Name = "Added Date (MM/DD/YYYY)")]
        public DateTime AddedDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new city name.")]
        [MaxLength(20, ErrorMessage = "A province name must be up to 20 characters.")]
        public string Province { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new city name.")]
        [MaxLength(50, ErrorMessage = "A city name must be up to 50 characters.")]
        [Display(Name = "City Name")]
        public string Name { get; set; }

        //Get the list of the child objects for the Child table references (1 City has many Posts)
        public List<Post> Posts { get; set; }
    }
}
