using System.ComponentModel.DataAnnotations;

namespace Fall2021_COMP2084_CourseProject.Models
{
    public class City
    {
        public int Id { get; set; }//Primary key

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a new city name.")]
        [Display(Name = "City Name")]
        public string Name { get; set; }
    }
}
