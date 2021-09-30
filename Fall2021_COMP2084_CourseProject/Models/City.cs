using System;
using System.ComponentModel.DataAnnotations;

namespace Fall2021_COMP2084_CourseProject.Models
{
    public class City
    {
        [Range(1, 999999)]//min:1, max:999999
        [Display(Name = "Post ID")]//Label that is actually printed on the browser
        public int Id { get; set; }

        [Display(Name = "Posted Date")]
        public string PostedDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select the area.")]
        [Display(Name = "Area")]
        public string Area { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please describe the room a bit.")]
        [Display(Name = "Description of The Room")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Your name must be entered.")]
        [Display(Name = "Author's Name")]
        public string AuthorName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Your phone number must be entered so a user can contact you.")]
        [Display(Name = "Author's Phone Number")]
        public string AuthorPhone { get; set; }
        [Display(Name = "Author's E-mail Address")]
        public string AuthorEmail { get; set; }
    }
}
