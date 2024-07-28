using momsAppApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace momsAppApi.Dal
{
    public class PersonalInformation
    {
            [Key]
            public int PersonalInformationId { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            public int AddressId { get; set; }

            public int ChildNumber { get; set; }

            [Required]
            public Gender ChildGender { get; set; }

            [Required]
            public DateTime ChildDateOfBirth { get; set; }

            [Required]
            [Phone]
            public string Phone { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public virtual Address Address { get; set; }
            public bool IsProfileComplete { get; set; } // To track profile completion
            public string PasswordHash { get; set; } // Added for password storage


        }
    }

