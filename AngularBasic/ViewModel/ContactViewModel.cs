using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularBasic.ViewModel
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile is Required")]
        [MaxLength(10, ErrorMessage = "Too long")]
        public string Mobile { get; set; }
    }
}
