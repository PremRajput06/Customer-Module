using System;
using System.Collections.Generic;

namespace CustomerModule.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string EmailId { get; set; } = null!;
    }
}
