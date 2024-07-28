using System.ComponentModel.DataAnnotations;

namespace momsAppApi.Dal
{
    public class Address
    {

            [Key]
            public int AddressId { get; set; }

            public int CityId { get; set; }

            public int DistrictId { get; set; }

            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public virtual City City { get; set; }
            public virtual District District { get; set; }
            public virtual ICollection<PersonalInformation> PersonalInformations { get; set; }

        }
    }



