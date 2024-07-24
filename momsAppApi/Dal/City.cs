using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace momsAppServer.Dal
{
        public class City
        {

        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
    }

