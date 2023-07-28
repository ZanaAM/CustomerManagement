using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Data
{
    public class Customer
    {
        public int Id { get; set; }
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
        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual IList<Address> Addresses { get; set; }

    }
}
