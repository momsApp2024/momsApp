using System.ComponentModel.DataAnnotations;

namespace momsAppApi.Dal
{
    public class District
    {
        
            [Key]
            public int DistrictId { get; set; }
            public string Name { get; set; }
            public virtual ICollection<City> Cities { get; set; }
            public virtual ICollection<Address> Addresses { get; set; }

        }
    }



