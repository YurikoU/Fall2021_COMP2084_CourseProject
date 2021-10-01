using System;
using System.ComponentModel.DataAnnotations;

namespace Fall2021_COMP2084_CourseProject.Models
{
    public class Post
    {
        //Primary key
        public int Id { get; set; }

        [DataType(DataType.Date)] //Show only the date
        [Display(Name = "Posted Date")]
        public DateTime PostedDate { get; set; }


        [Range(100, 999999, ErrorMessage = "The rent must be between $100 and $999,999")]
        [Display(Name = "Monthly rent (CAD$)")]
        public int Rent { get; set; } //Rent is optional because some rooms' rents are undisclosed.

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please describe the room a bit.")]
        [MaxLength(1500, ErrorMessage = "A description must be up to 1,500 characters.")]
        [Display(Name = "Description of the room")]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A phone number is required so a user can contact you.")]
        [MaxLength(20, ErrorMessage = "A phone number must be up to 20 characters.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")] //The input value must be a phone number
        [Display(Name = "Contact phone number")]
        public string PhoneOnPost { get; set; }

        [MaxLength(60, ErrorMessage = "An e-mail address must be up to 60 characters.")]
        [EmailAddress(ErrorMessage = "Please enter a properly formatted e-mail address.")] //The input value must be an email address
        [Display(Name = "Contact e-mail address")]
        public string EmailOnPost { get; set; }


        //Foreign keys
        //[Editable(false)]//UserId is read only.
        public string UserId { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }


        //Navigation Property: represents the parent object so Post can refer to the parent's property (1 City to many Posts)
        public City City { get; set; }
    }
}
