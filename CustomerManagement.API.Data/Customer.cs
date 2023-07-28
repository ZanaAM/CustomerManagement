using CustomerManagement.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Domain
{
    public class Customer: BaseEntity
    {
        [StringLength(20)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Forename { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(75)]
        public string EmailAddress { get; set; }
        [StringLength(15)]
        public string MobileNumber { get; set; }
       
        public bool IsActive { get; set; }
        public virtual IList<Address> Addresses { get; set; }

    }
}
